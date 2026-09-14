using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Application.Dtos;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IWarehouseService
    {
        WarehouseDto CreateWarehouse(CreateWarehouseDto createWarehouseDto);

        WarehouseDto UpdateWarehouse(UpdateWarehouseDto updateWarehouseDto);

        WarehouseDto DeleteWarehouse(Guid id);

        WarehouseDto GetWarehouse(Guid id);

        List<WarehouseDto> GetAllWarehouses(List<Guid>? warehouseIds);
    }
}
