using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IStockMovementRepository _stockMovementRepository;

        public StockMovementService(
            IStockMovementRepository stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
        }

        public StockMovementDto CreateStockMovement(CreateStockMovementDto createStockMovementDto)
        {
            if (createStockMovementDto.Quantity < 0)
            {
                throw new ArgumentException("Stock movement quantity cannot be negative.");
            }

            var stockMovement = new StockMovement
            {
                MovementType = createStockMovementDto.MovementType,
                ProductId = createStockMovementDto.ProductId,
                WarehouseId = createStockMovementDto.WarehouseId,
                Quantity = createStockMovementDto.Quantity,
            };

            var createdStockMovement = _stockMovementRepository.AddStockMovement(stockMovement);

            return ToDto(createdStockMovement);
        }

        public StockMovementDto DeleteStockMovement(Guid id)
        {
            var stockMovement = _stockMovementRepository.GetStockMovementById(id);
            if (stockMovement == null)
            {
                throw new KeyNotFoundException($"Stock movement with id '{id}' was not found.");
            }

            var deletedStockMovement = _stockMovementRepository.DeleteStockMovement(stockMovement);

            return ToDto(deletedStockMovement);
        }

        public List<StockMovementDto> GetAllStockMovements(List<Guid>? stockMovementIds)
        {
            var stockMovements = _stockMovementRepository.GetAllStockMovements(stockMovementIds);

            return stockMovements.Select(ToDto).ToList();
        }

        public StockMovementDto GetStockMovement(Guid id)
        {
            var stockMovement = _stockMovementRepository.GetStockMovementById(id);
            if (stockMovement == null)
            {
                throw new KeyNotFoundException($"Stock movement with id '{id}' was not found.");
            }

            return ToDto(stockMovement);
        }

        public List<StockMovementDto> GetStockMovementsByProduct(Guid productId)
        {
            var stockMovements = _stockMovementRepository.GetStockMovementsByProductId(productId);

            return stockMovements.Select(ToDto).ToList();
        }

        public List<StockMovementDto> GetStockMovementsByWarehouse(Guid warehouseId)
        {
            var stockMovements = _stockMovementRepository.GetStockMovementsByWarehouseId(warehouseId);

            return stockMovements.Select(ToDto).ToList();
        }

        public StockMovementDto UpdateStockMovement(UpdateStockMovementDto updateStockMovementDto)
        {
            if (updateStockMovementDto.Quantity < 0)
            {
                throw new ArgumentException("Stock movement quantity cannot be negative.");
            }

            var stockMovement = _stockMovementRepository.GetStockMovementById(updateStockMovementDto.Id);
            if (stockMovement == null)
            {
                throw new KeyNotFoundException($"Stock movement with id '{updateStockMovementDto.Id}' was not found.");
            }

            stockMovement.MovementType = updateStockMovementDto.MovementType;
            stockMovement.ProductId = updateStockMovementDto.ProductId;
            stockMovement.WarehouseId = updateStockMovementDto.WarehouseId;
            stockMovement.Quantity = updateStockMovementDto.Quantity;

            var updatedStockMovement = _stockMovementRepository.UpdateStockMovement(stockMovement);

            return ToDto(updatedStockMovement);
        }

        private static StockMovementDto ToDto(StockMovement stockMovement)
        {
            return new StockMovementDto
            {
                Id = stockMovement.Id,
                MovementType = stockMovement.MovementType,
                ProductId = stockMovement.ProductId,
                WarehouseId = stockMovement.WarehouseId,
                Quantity = stockMovement.Quantity
            };
        }
    }
}
