using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class UpdateUserDto : UserBaseDto
    {
        public Guid Id { get; set; }
    }
}
