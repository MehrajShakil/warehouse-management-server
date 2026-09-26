using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _dbContext;

        public RefreshTokenRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RefreshToken AddRefreshToken(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Add(refreshToken);

            _dbContext.SaveChanges();
            return refreshToken;
        }

        public RefreshToken UpdateRefreshToken(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Update(refreshToken);

            _dbContext.SaveChanges();
            return refreshToken;
        }

        public RefreshToken? GetRefreshTokenByToken(string token)
        {
            return _dbContext.RefreshTokens
                .Where(rt => rt.Token == token)
                .FirstOrDefault();
        }

        // IsActive is computed in C#, so the equivalent filter is written out for SQL translation.
        public List<RefreshToken> GetActiveRefreshTokensByUserId(Guid userId)
        {
            var now = DateTime.UtcNow;

            return _dbContext.RefreshTokens
                .Where(rt => rt.UserId == userId && rt.RevokedAt == null && rt.Expires > now)
                .ToList();
        }
    }
}
