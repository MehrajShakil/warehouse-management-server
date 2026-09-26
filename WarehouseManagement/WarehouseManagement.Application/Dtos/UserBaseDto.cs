using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class UserBaseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string? Password { get; set; }

        public string? PasswordHash { get; set; }

        public bool IsActive { get; set; }
    }
}
