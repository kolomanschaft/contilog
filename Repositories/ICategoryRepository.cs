using Contilog.Models;

namespace Contilog.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task<Category?> CreateCategory(Category category);
        Task<Category?> UpdateCategory(Category category);
    }
}
