using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Application.Dtos;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IProductService
    {
        ProductDto CreateProduct(CreateProductDto createProductDto);

        ProductDto UpdateProduct(UpdateProductDto updateProductDto);

        ProductDto DeleteProduct(Guid id);

        ProductDto GetProduct(Guid id);

        ProductDto GetProductBySku(string sku);

        List<ProductDto> SearchProducts(string name);

        List<ProductDto> GetAllProducts(List<Guid>? ProductIds);
    }
}
