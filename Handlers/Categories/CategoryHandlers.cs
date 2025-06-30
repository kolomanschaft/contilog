using Contilog.Models;
using Contilog.Repositories;

namespace Contilog.Handlers.Categories
{
    public class GetAllCategoriesHandler : IGetAllCategoriesHandler
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesRequest request)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return new GetAllCategoriesResponse(categories);
        }
    }

    public class UpdateCategoryHandler : IUpdateCategoryHandler
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request)
        {
            // Business logic: validate the request
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new UpdateCategoryResponse(null, false);
            }

            // Get the existing category
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            if (existingCategory == null)
            {
                return new UpdateCategoryResponse(null, false);
            }

            // Update the category
            existingCategory.Name = request.Name.Trim();

            // Save via repository
            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(existingCategory);
            return new UpdateCategoryResponse(updatedCategory, updatedCategory != null);
        }
    }
}
