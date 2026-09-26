using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IStockRepository
    {
        Stock AddStock(Stock stock);

        Stock UpdateStock(Stock stock);

        Stock DeleteStock(Stock stock);

        Stock? GetStockById(Guid id);

        Stock? GetStockByProductAndWarehouse(Guid productId, Guid warehouseId);

        List<Stock> GetStocksByProductId(Guid productId);

        List<Stock> GetStocksByWarehouseId(Guid warehouseId);

        List<Stock> GetAllStocks(List<Guid>? stockIds);
    }
}
