using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;
using Products.Repository.IRepository;

namespace Products.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _context;

        public CategoryRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(string id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateCategoryAsync(Category entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(string id)
        {
            var entity = _context.Categories.Find(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
