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
                new Category { Id = 1, Name = "Web Development", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 2, Name = "Backend", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 3, Name = "Database", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 4, Name = "Frontend", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 5, Name = "DevOps", CreatedDate = DateTime.Now.AddDays(-100), IsActive = true },
                new Category { Id = 6, Name = "Quality Assurance", CreatedDate = DateTime.Now.AddDays(-100), IsActive = false }
            };
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await Task.FromResult(_categories);
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(category);
        }

        public Task<Category?> CreateCategory(Category category)
        {
            // Generate a new ID
            var maxId = _categories.Any() ? _categories.Max(c => c.Id) : 0;
            category.Id = maxId + 1;
            category.CreatedDate = DateTime.Now;
            category.IsActive = true;

            // Add to the list
            _categories.Add(category);

            return Task.FromResult<Category?>(category);
        }

        public Task<Category?> UpdateCategory(Category category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory == null)
            {
                return Task.FromResult<Category?>(null);
            }

            // Update the existing category
            existingCategory.Name = category.Name;
            existingCategory.IsActive = category.IsActive;

            return Task.FromResult<Category?>(existingCategory);
        }
    }
}
