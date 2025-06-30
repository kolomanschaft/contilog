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
}
