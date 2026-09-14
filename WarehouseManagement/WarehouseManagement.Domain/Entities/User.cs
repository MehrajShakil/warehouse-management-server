using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
