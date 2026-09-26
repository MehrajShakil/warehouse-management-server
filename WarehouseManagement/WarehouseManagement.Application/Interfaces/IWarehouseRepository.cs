using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IWarehouseRepository
    {
        Warehouse AddWarehouse(Warehouse warehouse);

        Warehouse UpdateWarehouse(Warehouse warehouse);

        Warehouse DeleteWarehouse(Warehouse warehouse);

        Warehouse? GetWarehouseById(Guid id);

        List<Warehouse> GetAllWarehouses(List<Guid>? warehouseIds);
    }
}
