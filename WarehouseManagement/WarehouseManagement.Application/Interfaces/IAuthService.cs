using WarehouseManagement.Application.Dtos;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IAuthService
    {
        AuthResponseDto Register(CreateUserDto createUserDto);

        AuthResponseDto Login(LoginDto loginDto);

        AuthResponseDto RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto);

        void RevokeRefreshToken(RefreshTokenRequestDto refreshTokenRequestDto);
    }
}
