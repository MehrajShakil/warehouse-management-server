using System;
using System.Collections.Generic;
using WarehouseManagement.Application.Dtos;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IUserRoleService
    {
        void AssignRole(RoleAssignmentDto roleAssignmentDto);

        void RemoveRole(RoleAssignmentDto roleAssignmentDto);

        List<RoleDto> GetRolesForUser(Guid userId);

        List<UserDto> GetUsersInRole(Guid roleId);
    }
}
