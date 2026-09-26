using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _dbContext;

        public ProductRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product AddProduct(Product product)
        {
            _dbContext.Products.Add(product);

            _dbContext.SaveChanges();
            return product;
        }

        public Product DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);

            _dbContext.SaveChanges();
            return product;
        }

        public List<Product> GetAllProducts(List<Guid>? productIds)
        {
            var products = _dbContext.Products
                    .Where(p => productIds == null || productIds.Count == 0 || productIds.Contains(p.Id))
                    .ToList();

            return products;
        }

        public Product? GetProductById(Guid id)
        {
            return _dbContext.Products
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public Product? GetProductBySku(string sku)
        {
            return _dbContext.Products
                .Where(p => p.SKU == sku)
                .FirstOrDefault();
        }

        public List<Product> SearchProductsByName(string name)
        {
            return _dbContext.Products
                .Where(p => EF.Functions.ILike(p.Name, $"%{name}%"))
                .ToList();
        }

        public Product UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);

            _dbContext.SaveChanges();
            return product;
        }
    }
}
