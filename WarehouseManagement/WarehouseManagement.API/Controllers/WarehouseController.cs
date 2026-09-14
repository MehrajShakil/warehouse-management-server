using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;

namespace WarehouseManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(
            IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }


        [HttpPost]
        public IActionResult CreateWarehouse(
            [FromBody] CreateWarehouseDto createWarehouseDto)
        {
            var warehouseDto = _warehouseService.CreateWarehouse(createWarehouseDto);
            return Ok(warehouseDto);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetWarehouse(Guid id)
        {
            var warehouseDto = _warehouseService.GetWarehouse(id);
            return Ok(warehouseDto);
        }

        [HttpGet]
        public IActionResult GetAllWarehouses([FromQuery] List<Guid>? warehouseIds)
        {
            var warehouseDtos = _warehouseService.GetAllWarehouses(warehouseIds);
            return Ok(warehouseDtos);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateWarehouse(
            Guid id,
            [FromBody] UpdateWarehouseDto updateWarehouseDto)
        {
            if (id != updateWarehouseDto.Id)
            {
                return BadRequest("Route id does not match the id in the request body.");
            }

            var warehouseDto = _warehouseService.UpdateWarehouse(updateWarehouseDto);
            return Ok(warehouseDto);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteWarehouse(Guid id)
        {
            var warehouseDto = _warehouseService.DeleteWarehouse(id);
            return Ok(warehouseDto);
        }
    }
}
