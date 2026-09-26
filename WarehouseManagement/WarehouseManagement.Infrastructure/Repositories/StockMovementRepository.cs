using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly AppDbContext _dbContext;

        public StockMovementRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public StockMovement AddStockMovement(StockMovement stockMovement)
        {
            _dbContext.StockMovements.Add(stockMovement);

            _dbContext.SaveChanges();
            return stockMovement;
        }

        public StockMovement UpdateStockMovement(StockMovement stockMovement)
        {
            _dbContext.StockMovements.Update(stockMovement);

            _dbContext.SaveChanges();
            return stockMovement;
        }

        public StockMovement DeleteStockMovement(StockMovement stockMovement)
        {
            _dbContext.StockMovements.Remove(stockMovement);

            _dbContext.SaveChanges();
            return stockMovement;
        }

        public StockMovement? GetStockMovementById(Guid id)
        {
            return _dbContext.StockMovements
                .Where(sm => sm.Id == id)
                .FirstOrDefault();
        }

        public List<StockMovement> GetStockMovementsByProductId(Guid productId)
        {
            return _dbContext.StockMovements
                .Where(sm => sm.ProductId == productId)
                .OrderByDescending(sm => sm.CreatedAt)
                .ToList();
        }

        public List<StockMovement> GetStockMovementsByWarehouseId(Guid warehouseId)
        {
            return _dbContext.StockMovements
                .Where(sm => sm.WarehouseId == warehouseId)
                .OrderByDescending(sm => sm.CreatedAt)
                .ToList();
        }

        public List<StockMovement> GetAllStockMovements(List<Guid>? stockMovementIds)
        {
            return _dbContext.StockMovements
                .Where(sm => stockMovementIds == null || stockMovementIds.Count == 0 || stockMovementIds.Contains(sm.Id))
                .OrderByDescending(sm => sm.CreatedAt)
                .ToList();
        }
    }
}
