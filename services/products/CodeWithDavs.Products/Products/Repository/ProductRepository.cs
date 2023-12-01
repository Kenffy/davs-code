using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Products.Data;
using Products.Models;
using Products.Repository.IRepository;
using Slugify;

namespace Products.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;
        private readonly SlugHelper _slugify;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
            _slugify = new SlugHelper();
        }

        public async Task CreateProductAsync(Product entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.Slug = _slugify.GenerateSlug(entity.Name);
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
            entity.Slug = _slugify.GenerateSlug(entity.Name);
            entity.UpdatedAt = DateTime.Now;
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
