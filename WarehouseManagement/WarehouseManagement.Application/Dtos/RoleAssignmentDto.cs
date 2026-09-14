using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class RoleAssignmentDto
    {
        public required Guid UserId { get; set; }

        public required Guid RoleId { get; set; }
    }
}
