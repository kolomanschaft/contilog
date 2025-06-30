using Contilog.Models;

namespace Contilog.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> UpdateCategoryAsync(Category category);
    }
}
