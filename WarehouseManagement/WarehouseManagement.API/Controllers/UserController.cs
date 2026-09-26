using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Application.Services;

namespace WarehouseManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public UserController(
            IUserService userService,
            IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public IActionResult CreateUser(
            [FromBody] CreateUserDto createUserDto)
        {
            var userDto = _userService.CreateUser(createUserDto);

            return Ok(userDto);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetUser(Guid id)
        {
            var userDto = _userService.GetUser(id);
            return Ok(userDto);
        }

        [HttpGet]
        public IActionResult GetAllUsers([FromQuery] List<Guid>? userIds)
        {
            var userDtos = _userService.GetAllUsers(userIds);
            return Ok(userDtos);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateUser(
            Guid id,
            [FromBody] UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id)
            {
                return BadRequest("Route id does not match the id in the request body.");
            }

            var userDto = _userService.UpdateUser(updateUserDto);
            return Ok(userDto);
        }

        [HttpPatch("{id:guid}/change-password")]
        public IActionResult ChangePassword(
            Guid id,
            [FromBody] ChangePasswordDto changePasswordDto)
        {
            var userDto = _userService.ChangePassword(id, changePasswordDto);
            return Ok(userDto);
        }

        [HttpPatch("{id:guid}/active")]
        public IActionResult SetUserActive(
            Guid id,
            [FromBody] SetUserActiveDto setUserActiveDto)
        {
            var userDto = _userService.SetUserActive(id, setUserActiveDto);
            return Ok(userDto);
        }

        [HttpGet("{id:guid}/roles")]
        public IActionResult GetUserRoles(Guid id)
        {
            var roleDtos = _userRoleService.GetRolesForUser(id);
            return Ok(roleDtos);
        }

        [HttpPost("{id:guid}/roles/{roleId:guid}")]
        public IActionResult AssignRole(Guid id, Guid roleId)
        {
            _userRoleService.AssignRole(new RoleAssignmentDto { UserId = id, RoleId = roleId });
            return NoContent();
        }

        [HttpDelete("{id:guid}/roles/{roleId:guid}")]
        public IActionResult RemoveRole(Guid id, Guid roleId)
        {
            _userRoleService.RemoveRole(new RoleAssignmentDto { UserId = id, RoleId = roleId });
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            var userDto = _userService.DeleteUser(id);
            return Ok(userDto);
        }
    }
}
