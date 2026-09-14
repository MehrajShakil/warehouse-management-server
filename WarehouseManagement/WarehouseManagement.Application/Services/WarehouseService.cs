using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseService(
            IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public WarehouseDto CreateWarehouse(CreateWarehouseDto createWarehouseDto)
        {
            var warehouse = new Warehouse
            {
                Name = createWarehouseDto.Name,
                Location = createWarehouseDto.Location,
            };

            var createdWarehouse = _warehouseRepository.AddWarehouse(warehouse);

            return ToDto(createdWarehouse);
        }

        public WarehouseDto DeleteWarehouse(Guid id)
        {
            var warehouse = _warehouseRepository.GetWarehouseById(id);
            if (warehouse == null)
            {
                throw new KeyNotFoundException($"Warehouse with id '{id}' was not found.");
            }

            var deletedWarehouse = _warehouseRepository.DeleteWarehouse(warehouse);

            return ToDto(deletedWarehouse);
        }

        public List<WarehouseDto> GetAllWarehouses(List<Guid>? warehouseIds)
        {
            var warehouses = _warehouseRepository.GetAllWarehouses(warehouseIds);

            return warehouses.Select(ToDto).ToList();
        }

        public WarehouseDto GetWarehouse(Guid id)
        {
            var warehouse = _warehouseRepository.GetWarehouseById(id);
            if (warehouse == null)
            {
                throw new KeyNotFoundException($"Warehouse with id '{id}' was not found.");
            }

            return ToDto(warehouse);
        }

        public WarehouseDto UpdateWarehouse(UpdateWarehouseDto updateWarehouseDto)
        {
            var warehouse = _warehouseRepository.GetWarehouseById(updateWarehouseDto.Id);
            if (warehouse == null)
            {
                throw new KeyNotFoundException($"Warehouse with id '{updateWarehouseDto.Id}' was not found.");
            }

            warehouse.Name = updateWarehouseDto.Name;
            warehouse.Location = updateWarehouseDto.Location;

            var updatedWarehouse = _warehouseRepository.UpdateWarehouse(warehouse);

            return ToDto(updatedWarehouse);
        }

        private static WarehouseDto ToDto(Warehouse warehouse)
        {
            return new WarehouseDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Location = warehouse.Location
            };
        }
    }
}
