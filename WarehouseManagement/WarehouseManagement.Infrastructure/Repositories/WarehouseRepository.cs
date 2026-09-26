using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _dbContext;

        public WarehouseRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Warehouse AddWarehouse(Warehouse warehouse)
        {
            _dbContext.Warehouses.Add(warehouse);

            _dbContext.SaveChanges();
            return warehouse;
        }

        public Warehouse UpdateWarehouse(Warehouse warehouse)
        {
            _dbContext.Warehouses.Update(warehouse);

            _dbContext.SaveChanges();
            return warehouse;
        }

        public Warehouse DeleteWarehouse(Warehouse warehouse)
        {
            _dbContext.Warehouses.Remove(warehouse);

            _dbContext.SaveChanges();
            return warehouse;
        }

        public Warehouse? GetWarehouseById(Guid id)
        {
            return _dbContext.Warehouses
                .Where(w => w.Id == id)
                .FirstOrDefault();
        }

        public List<Warehouse> GetAllWarehouses(List<Guid>? warehouseIds)
        {
            return _dbContext.Warehouses
                .Where(w => warehouseIds == null || warehouseIds.Count == 0 || warehouseIds.Contains(w.Id))
                .ToList();
        }
    }
}
