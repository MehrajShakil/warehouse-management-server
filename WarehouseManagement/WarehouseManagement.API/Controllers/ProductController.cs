using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;

namespace WarehouseManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(
            IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public IActionResult CreateProduct(
            [FromBody] CreateProductDto createProductDto)
        {
            var productDto = _productService.CreateProduct(createProductDto);
            return Ok(productDto);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetProduct(Guid id)
        {
            var productDto = _productService.GetProduct(id);
            return Ok(productDto);
        }

        [HttpGet("by-sku/{sku}")]
        public IActionResult GetProductBySku(string sku)
        {
            var productDto = _productService.GetProductBySku(sku);
            return Ok(productDto);
        }

        [HttpGet("search")]
        public IActionResult SearchProducts([FromQuery] string name)
        {
            var productDtos = _productService.SearchProducts(name);
            return Ok(productDtos);
        }

        [HttpGet]
        public IActionResult GetAllProducts([FromQuery] List<Guid>? productIds)
        {
            var productDtos = _productService.GetAllProducts(productIds);
            return Ok(productDtos);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateProduct(
            Guid id,
            [FromBody] UpdateProductDto updateProductDto)
        {
            if (id != updateProductDto.Id)
            {
                return BadRequest("Route id does not match the id in the request body.");
            }

            var productDto = _productService.UpdateProduct(updateProductDto);
            return Ok(productDto);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var productDto = _productService.DeleteProduct(id);
            return Ok(productDto);
        }
    }
}
