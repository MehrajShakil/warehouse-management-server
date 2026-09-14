using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseManagement.Application.Dtos;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;

        public ProductService(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductDto CreateProduct(CreateProductDto createProductDto)
        {
            if(createProductDto.UnitPrice < 0)
            {
                throw new ArgumentException("Product unit price cannot be negative.");
            }

            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                SKU = createProductDto.SKU,
                UnitPrice = createProductDto.UnitPrice,
            };

            var createdProduct = _productRepository.AddProduct(product);

            return ToDto(createdProduct);
        }

        public ProductDto DeleteProduct(Guid id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with id '{id}' was not found.");
            }

            var deletedProduct = _productRepository.DeleteProduct(product);

            return ToDto(deletedProduct);
        }

        public List<ProductDto> GetAllProducts(List<Guid>? ProductIds)
        {
            var products = _productRepository.GetAllProducts(ProductIds);

            return products.Select(ToDto).ToList();
        }

        public ProductDto GetProduct(Guid id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with id '{id}' was not found.");
            }

            return ToDto(product);
        }

        public ProductDto GetProductBySku(string sku)
        {
            var product = _productRepository.GetProductBySku(sku);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with SKU '{sku}' was not found.");
            }

            return ToDto(product);
        }

        public List<ProductDto> SearchProducts(string name)
        {
            var products = _productRepository.SearchProductsByName(name);

            return products.Select(ToDto).ToList();
        }

        public ProductDto UpdateProduct(UpdateProductDto updateProductDto)
        {
            if (updateProductDto.UnitPrice < 0)
            {
                throw new ArgumentException("Product unit price cannot be negative.");
            }

            var product = _productRepository.GetProductById(updateProductDto.Id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with id '{updateProductDto.Id}' was not found.");
            }

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.SKU = updateProductDto.SKU;
            product.UnitPrice = updateProductDto.UnitPrice;

            var updatedProduct = _productRepository.UpdateProduct(product);

            return ToDto(updatedProduct);
        }

        private static ProductDto ToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                UnitPrice = product.UnitPrice
            };
        }
    }
}
