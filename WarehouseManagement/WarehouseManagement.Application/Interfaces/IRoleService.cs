using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Application.Dtos;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IRoleService
    {
        RoleDto CreateRole(CreateRoleDto createRoleDto);

        RoleDto UpdateRole(UpdateRoleDto updateRoleDto);

        RoleDto DeleteRole(Guid id);

        RoleDto GetRole(Guid id);

        List<RoleDto> GetAllRoles(List<Guid>? roleIds);
    }
}
