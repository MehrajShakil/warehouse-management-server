using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class UserDto : UserBaseDto
    {
        public Guid Id { get; set; }
    }
}
