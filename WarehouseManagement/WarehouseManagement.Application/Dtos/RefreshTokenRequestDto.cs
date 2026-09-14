using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class RefreshTokenRequestDto
    {
        public required string RefreshToken { get; set; }
    }
}
