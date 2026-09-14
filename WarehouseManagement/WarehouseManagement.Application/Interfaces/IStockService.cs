using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Application.Dtos;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IStockService
    {
        StockDto CreateStock(CreateStockDto createStockDto);

        StockDto UpdateStock(UpdateStockDto updateStockDto);

        StockDto DeleteStock(Guid id);

        StockDto GetStock(Guid id);

        List<StockDto> GetStocksByProduct(Guid productId);

        List<StockDto> GetStocksByWarehouse(Guid warehouseId);

        StockDto AdjustStock(Guid id, AdjustStockDto adjustStockDto);

        List<StockDto> GetAllStocks(List<Guid>? stockIds);
    }
}
