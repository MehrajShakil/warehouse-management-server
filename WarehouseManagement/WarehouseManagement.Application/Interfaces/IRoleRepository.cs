using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IRoleRepository
    {
        Role AddRole(Role role);

        Role UpdateRole(Role role);

        Role DeleteRole(Role role);

        Role? GetRoleById(Guid id);

        List<Role> GetAllRoles(List<Guid>? roleIds);
    }
}
