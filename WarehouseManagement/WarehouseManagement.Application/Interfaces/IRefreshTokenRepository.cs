using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        RefreshToken AddRefreshToken(RefreshToken refreshToken);

        RefreshToken UpdateRefreshToken(RefreshToken refreshToken);

        RefreshToken? GetRefreshTokenByToken(string token);

        List<RefreshToken> GetActiveRefreshTokensByUserId(Guid userId);
    }
}
