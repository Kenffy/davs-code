using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;
using Products.Repository.IRepository;

namespace Products.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(Product entity)
        {
            entity.CreatedAt = DateTime.Now;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(string id)
        {
            var entity = _context.Categories.Find(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllProductAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateProductAsync(Product entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
