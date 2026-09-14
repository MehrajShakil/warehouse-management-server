using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;

namespace WarehouseManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockMovementController : ControllerBase
    {
        private readonly IStockMovementService _stockMovementService;

        public StockMovementController(
            IStockMovementService stockMovementService)
        {
            _stockMovementService = stockMovementService;
        }


        [HttpPost]
        public IActionResult CreateStockMovement(
            [FromBody] CreateStockMovementDto createStockMovementDto)
        {
            var stockMovementDto = _stockMovementService.CreateStockMovement(createStockMovementDto);
            return Ok(stockMovementDto);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetStockMovement(Guid id)
        {
            var stockMovementDto = _stockMovementService.GetStockMovement(id);
            return Ok(stockMovementDto);
        }

        [HttpGet("by-product/{productId:guid}")]
        public IActionResult GetStockMovementsByProduct(Guid productId)
        {
            var stockMovementDtos = _stockMovementService.GetStockMovementsByProduct(productId);
            return Ok(stockMovementDtos);
        }

        [HttpGet("by-warehouse/{warehouseId:guid}")]
        public IActionResult GetStockMovementsByWarehouse(Guid warehouseId)
        {
            var stockMovementDtos = _stockMovementService.GetStockMovementsByWarehouse(warehouseId);
            return Ok(stockMovementDtos);
        }

        [HttpGet]
        public IActionResult GetAllStockMovements([FromQuery] List<Guid>? stockMovementIds)
        {
            var stockMovementDtos = _stockMovementService.GetAllStockMovements(stockMovementIds);
            return Ok(stockMovementDtos);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateStockMovement(
            Guid id,
            [FromBody] UpdateStockMovementDto updateStockMovementDto)
        {
            if (id != updateStockMovementDto.Id)
            {
                return BadRequest("Route id does not match the id in the request body.");
            }

            var stockMovementDto = _stockMovementService.UpdateStockMovement(updateStockMovementDto);
            return Ok(stockMovementDto);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteStockMovement(Guid id)
        {
            var stockMovementDto = _stockMovementService.DeleteStockMovement(id);
            return Ok(stockMovementDto);
        }
    }
}
