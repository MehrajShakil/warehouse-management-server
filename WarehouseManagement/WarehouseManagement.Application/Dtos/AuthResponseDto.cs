using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class AuthResponseDto
    {
        public required Guid UserId { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string AccessToken { get; set; }

        public required DateTime AccessTokenExpires { get; set; }

        public required string RefreshToken { get; set; }

        public required DateTime RefreshTokenExpires { get; set; }
    }
}
