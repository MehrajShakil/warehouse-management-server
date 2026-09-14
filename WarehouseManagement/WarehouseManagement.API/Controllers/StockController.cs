using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;

namespace WarehouseManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(
            IStockService stockService)
        {
            _stockService = stockService;
        }


        [HttpPost]
        public IActionResult CreateStock(
            [FromBody] CreateStockDto createStockDto)
        {
            var stockDto = _stockService.CreateStock(createStockDto);
            return Ok(stockDto);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetStock(Guid id)
        {
            var stockDto = _stockService.GetStock(id);
            return Ok(stockDto);
        }

        [HttpGet("by-product/{productId:guid}")]
        public IActionResult GetStocksByProduct(Guid productId)
        {
            var stockDtos = _stockService.GetStocksByProduct(productId);
            return Ok(stockDtos);
        }

        [HttpGet("by-warehouse/{warehouseId:guid}")]
        public IActionResult GetStocksByWarehouse(Guid warehouseId)
        {
            var stockDtos = _stockService.GetStocksByWarehouse(warehouseId);
            return Ok(stockDtos);
        }

        [HttpGet]
        public IActionResult GetAllStocks([FromQuery] List<Guid>? stockIds)
        {
            var stockDtos = _stockService.GetAllStocks(stockIds);
            return Ok(stockDtos);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateStock(
            Guid id,
            [FromBody] UpdateStockDto updateStockDto)
        {
            if (id != updateStockDto.Id)
            {
                return BadRequest("Route id does not match the id in the request body.");
            }

            var stockDto = _stockService.UpdateStock(updateStockDto);
            return Ok(stockDto);
        }

        [HttpPatch("{id:guid}/adjust")]
        public IActionResult AdjustStock(
            Guid id,
            [FromBody] AdjustStockDto adjustStockDto)
        {
            var stockDto = _stockService.AdjustStock(id, adjustStockDto);
            return Ok(stockDto);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteStock(Guid id)
        {
            var stockDto = _stockService.DeleteStock(id);
            return Ok(stockDto);
        }
    }
}
