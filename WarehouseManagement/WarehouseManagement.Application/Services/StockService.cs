using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(
            IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public StockDto CreateStock(CreateStockDto createStockDto)
        {
            if (createStockDto.Quantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot be negative.");
            }

            var stock = new Stock
            {
                ProductId = createStockDto.ProductId,
                WarehouseId = createStockDto.WarehouseId,
                Quantity = createStockDto.Quantity,
            };

            var createdStock = _stockRepository.AddStock(stock);

            return ToDto(createdStock);
        }

        public StockDto DeleteStock(Guid id)
        {
            var stock = _stockRepository.GetStockById(id);
            if (stock == null)
            {
                throw new KeyNotFoundException($"Stock with id '{id}' was not found.");
            }

            var deletedStock = _stockRepository.DeleteStock(stock);

            return ToDto(deletedStock);
        }

        public List<StockDto> GetAllStocks(List<Guid>? stockIds)
        {
            var stocks = _stockRepository.GetAllStocks(stockIds);

            return stocks.Select(ToDto).ToList();
        }

        public StockDto GetStock(Guid id)
        {
            var stock = _stockRepository.GetStockById(id);
            if (stock == null)
            {
                throw new KeyNotFoundException($"Stock with id '{id}' was not found.");
            }

            return ToDto(stock);
        }

        public List<StockDto> GetStocksByProduct(Guid productId)
        {
            var stocks = _stockRepository.GetStocksByProductId(productId);

            return stocks.Select(ToDto).ToList();
        }

        public List<StockDto> GetStocksByWarehouse(Guid warehouseId)
        {
            var stocks = _stockRepository.GetStocksByWarehouseId(warehouseId);

            return stocks.Select(ToDto).ToList();
        }

        public StockDto AdjustStock(Guid id, AdjustStockDto adjustStockDto)
        {
            var stock = _stockRepository.GetStockById(id);
            if (stock == null)
            {
                throw new KeyNotFoundException($"Stock with id '{id}' was not found.");
            }

            var newQuantity = stock.Quantity + adjustStockDto.QuantityDelta;
            if (newQuantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot go below zero.");
            }

            stock.Quantity = newQuantity;

            var updatedStock = _stockRepository.UpdateStock(stock);

            return ToDto(updatedStock);
        }

        public StockDto UpdateStock(UpdateStockDto updateStockDto)
        {
            if (updateStockDto.Quantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot be negative.");
            }

            var stock = _stockRepository.GetStockById(updateStockDto.Id);
            if (stock == null)
            {
                throw new KeyNotFoundException($"Stock with id '{updateStockDto.Id}' was not found.");
            }

            stock.ProductId = updateStockDto.ProductId;
            stock.WarehouseId = updateStockDto.WarehouseId;
            stock.Quantity = updateStockDto.Quantity;

            var updatedStock = _stockRepository.UpdateStock(stock);

            return ToDto(updatedStock);
        }

        private static StockDto ToDto(Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                WarehouseId = stock.WarehouseId,
                Quantity = stock.Quantity
            };
        }
    }
}
