using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(
            IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public RoleDto CreateRole(CreateRoleDto createRoleDto)
        {
            var role = new Role
            {
                Name = createRoleDto.Name,
            };

            var createdRole = _roleRepository.AddRole(role);

            return ToDto(createdRole);
        }

        public RoleDto DeleteRole(Guid id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with id '{id}' was not found.");
            }

            var deletedRole = _roleRepository.DeleteRole(role);

            return ToDto(deletedRole);
        }

        public List<RoleDto> GetAllRoles(List<Guid>? roleIds)
        {
            var roles = _roleRepository.GetAllRoles(roleIds);

            return roles.Select(ToDto).ToList();
        }

        public RoleDto GetRole(Guid id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with id '{id}' was not found.");
            }

            return ToDto(role);
        }

        public RoleDto UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var role = _roleRepository.GetRoleById(updateRoleDto.Id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with id '{updateRoleDto.Id}' was not found.");
            }

            role.Name = updateRoleDto.Name;

            var updatedRole = _roleRepository.UpdateRole(role);

            return ToDto(updatedRole);
        }

        private static RoleDto ToDto(Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
