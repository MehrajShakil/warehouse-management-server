using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class UpdateRoleDto : RoleBaseDto
    {
        public Guid Id { get; set; }
    }
}
