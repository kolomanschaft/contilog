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

    public class GetCategoryByIdHandler : IGetCategoryByIdHandler
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            return new GetCategoryByIdResponse(category);
        }
    }
}
