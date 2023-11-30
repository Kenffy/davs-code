using Products.Models;
using Products.Models.Dto;

namespace Products.Repository.IRepository
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product entity);
        Task UpdateProductAsync(Product entity);
        Task DeleteProductAsync(string id);
        Task<Product> GetProductAsync(string id);
        Task<IReadOnlyList<Product>> GetAllProductAsync();
    }
}
