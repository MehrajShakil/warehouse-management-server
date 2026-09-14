using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Application.Dtos;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IStockMovementService
    {
        StockMovementDto CreateStockMovement(CreateStockMovementDto createStockMovementDto);

        StockMovementDto UpdateStockMovement(UpdateStockMovementDto updateStockMovementDto);

        StockMovementDto DeleteStockMovement(Guid id);

        StockMovementDto GetStockMovement(Guid id);

        List<StockMovementDto> GetStockMovementsByProduct(Guid productId);

        List<StockMovementDto> GetStockMovementsByWarehouse(Guid warehouseId);

        List<StockMovementDto> GetAllStockMovements(List<Guid>? stockMovementIds);
    }
}
