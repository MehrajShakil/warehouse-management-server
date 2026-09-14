using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserRoleService(
            IUserRoleRepository userRoleRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public void AssignRole(RoleAssignmentDto roleAssignmentDto)
        {
            var user = _userRepository.GetUserById(roleAssignmentDto.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id '{roleAssignmentDto.UserId}' was not found.");
            }

            var role = _roleRepository.GetRoleById(roleAssignmentDto.RoleId);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with id '{roleAssignmentDto.RoleId}' was not found.");
            }

            if (_userRoleRepository.HasRole(roleAssignmentDto.UserId, roleAssignmentDto.RoleId))
            {
                throw new InvalidOperationException("User already has this role.");
            }

            _userRoleRepository.AssignRole(new UserRoles
            {
                UserId = roleAssignmentDto.UserId,
                RoleId = roleAssignmentDto.RoleId
            });
        }

        public void RemoveRole(RoleAssignmentDto roleAssignmentDto)
        {
            var removed = _userRoleRepository.RemoveRole(roleAssignmentDto.UserId, roleAssignmentDto.RoleId);
            if (!removed)
            {
                throw new KeyNotFoundException("The user does not have this role assigned.");
            }
        }

        public List<RoleDto> GetRolesForUser(Guid userId)
        {
            var roles = _userRoleRepository.GetRolesByUserId(userId);

            return roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
        }

        public List<UserDto> GetUsersInRole(Guid roleId)
        {
            var users = _userRoleRepository.GetUsersByRoleId(roleId);

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = string.Empty,
                PasswordHash = string.Empty,
                IsActive = user.IsActive
            }).ToList();
        }
    }
}
