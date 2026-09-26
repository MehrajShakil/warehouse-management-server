using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _dbContext;

        public StockRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Stock AddStock(Stock stock)
        {
            _dbContext.Stocks.Add(stock);

            _dbContext.SaveChanges();
            return stock;
        }

        public Stock UpdateStock(Stock stock)
        {
            _dbContext.Stocks.Update(stock);

            _dbContext.SaveChanges();
            return stock;
        }

        public Stock DeleteStock(Stock stock)
        {
            _dbContext.Stocks.Remove(stock);

            _dbContext.SaveChanges();
            return stock;
        }

        public Stock? GetStockById(Guid id)
        {
            return _dbContext.Stocks
                .Where(s => s.Id == id)
                .FirstOrDefault();
        }

        public Stock? GetStockByProductAndWarehouse(Guid productId, Guid warehouseId)
        {
            return _dbContext.Stocks
                .Where(s => s.ProductId == productId && s.WarehouseId == warehouseId)
                .FirstOrDefault();
        }

        public List<Stock> GetStocksByProductId(Guid productId)
        {
            return _dbContext.Stocks
                .Where(s => s.ProductId == productId)
                .ToList();
        }

        public List<Stock> GetStocksByWarehouseId(Guid warehouseId)
        {
            return _dbContext.Stocks
                .Where(s => s.WarehouseId == warehouseId)
                .ToList();
        }

        public List<Stock> GetAllStocks(List<Guid>? stockIds)
        {
            return _dbContext.Stocks
                .Where(s => stockIds == null || stockIds.Count == 0 || stockIds.Contains(s.Id))
                .ToList();
        }
    }
}
