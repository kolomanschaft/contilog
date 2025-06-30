using Contilog.Models;

namespace Contilog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories;

        public CategoryRepository()
        {
            _categories = new List<Category>
            {
                new Category { Id = 1, Name = "Web Development", Description = "Frontend and full-stack web development topics", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 2, Name = "Backend", Description = "Server-side development and API topics", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 3, Name = "Database", Description = "Database design, optimization, and management", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 4, Name = "Frontend", Description = "Client-side development and user interfaces", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 5, Name = "DevOps", Description = "Development operations and deployment", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 6, Name = "Quality Assurance", Description = "Testing strategies and quality control", CreatedDate = DateTime.Now.AddDays(-100), IsActive = false }
            };
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await Task.FromResult(_categories);
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(category);
        }

        public Task<Category?> UpdateCategoryAsync(Category category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory == null)
            {
                return Task.FromResult<Category?>(null);
            }

            // Update the existing category
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.IsActive = category.IsActive;

            return Task.FromResult<Category?>(existingCategory);
        }
    }
}
