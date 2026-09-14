using System;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Interfaces
{
    public interface ITokenService
    {
        (string Token, DateTime Expires) GenerateAccessToken(User user);

        (string Token, DateTime Expires) GenerateRefreshToken();
    }
}
