using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IUserRoleRepository
    {
        UserRoles AssignRole(UserRoles userRole);

        bool RemoveRole(Guid userId, Guid roleId);

        bool HasRole(Guid userId, Guid roleId);

        List<Role> GetRolesByUserId(Guid userId);

        List<User> GetUsersByRoleId(Guid roleId);
    }
}
