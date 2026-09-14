using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;

namespace WarehouseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(
            IAuthService authService,
            IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(
            [FromBody] CreateUserDto createUserDto)
        {
            var authResponse = _authService.Register(createUserDto);
            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(
            [FromBody] LoginDto loginDto)
        {
            var authResponse = _authService.Login(loginDto);
            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh(
            [FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var authResponse = _authService.RefreshToken(refreshTokenRequestDto);
            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("revoke")]
        public IActionResult Revoke(
            [FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            _authService.RevokeRefreshToken(refreshTokenRequestDto);
            return NoContent();
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var userDto = _userService.GetUser(userId);
            return Ok(userDto);
        }
    }
}
