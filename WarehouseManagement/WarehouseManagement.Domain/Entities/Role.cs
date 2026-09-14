using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Domain.Entities
{
    public class Role : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
