using Products.Models;

namespace Products.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(string id);
        Task<Category> GetCategoryAsync(string id);
        Task<IReadOnlyList<Category>> GetAllCategoryAsync();
    }
}
