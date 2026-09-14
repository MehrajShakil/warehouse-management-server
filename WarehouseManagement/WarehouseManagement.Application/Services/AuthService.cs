using System;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Application.Security;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }

        public AuthResponseDto Register(CreateUserDto createUserDto)
        {
            var existingUser = _userRepository.GetUserByUserNameOrEmail(createUserDto.UserName)
                ?? _userRepository.GetUserByUserNameOrEmail(createUserDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this username or email already exists.");
            }

            var user = new User
            {
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                Password = string.Empty,
                PasswordHash = PasswordHasher.Hash(createUserDto.Password),
                IsActive = true
            };

            var createdUser = _userRepository.AddUser(user);

            return IssueTokens(createdUser);
        }

        public AuthResponseDto Login(LoginDto loginDto)
        {
            var user = _userRepository.GetUserByUserNameOrEmail(loginDto.UserNameOrEmail);
            if (user == null || !PasswordHasher.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username/email or password.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("This user account is inactive.");
            }

            return IssueTokens(user);
        }

        public AuthResponseDto RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var storedToken = _refreshTokenRepository.GetRefreshTokenByToken(refreshTokenRequestDto.RefreshToken);
            if (storedToken == null || !storedToken.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }

            var user = _userRepository.GetUserById(storedToken.UserId);
            if (user == null || !user.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }

            var response = IssueTokens(user);

            storedToken.RevokedAt = DateTime.UtcNow;
            storedToken.ReplacedByToken = response.RefreshToken;
            _refreshTokenRepository.UpdateRefreshToken(storedToken);

            return response;
        }

        public void RevokeRefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var storedToken = _refreshTokenRepository.GetRefreshTokenByToken(refreshTokenRequestDto.RefreshToken);
            if (storedToken == null || !storedToken.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }

            storedToken.RevokedAt = DateTime.UtcNow;
            _refreshTokenRepository.UpdateRefreshToken(storedToken);
        }

        private AuthResponseDto IssueTokens(User user)
        {
            var (accessToken, accessTokenExpires) = _tokenService.GenerateAccessToken(user);
            var (refreshToken, refreshTokenExpires) = _tokenService.GenerateRefreshToken();

            _refreshTokenRepository.AddRefreshToken(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Expires = refreshTokenExpires
            });

            return new AuthResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                AccessToken = accessToken,
                AccessTokenExpires = accessTokenExpires,
                RefreshToken = refreshToken,
                RefreshTokenExpires = refreshTokenExpires
            };
        }
    }
}
