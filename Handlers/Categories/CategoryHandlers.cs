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
            var categories = await _categoryRepository.GetAllCategories();
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
            var category = await _categoryRepository.GetCategoryById(request.CategoryId);
            return new GetCategoryByIdResponse(category);
        }
    }

    public class CreateCategoryHandler : ICreateCategoryHandler
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request)
        {
            // Business logic: validate the request
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new CreateCategoryResponse(null, false);
            }

            // Create the new category
            var newCategory = new Category
            {
                Name = request.Name.Trim(),
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            // Save via repository
            var createdCategory = await _categoryRepository.CreateCategory(newCategory);
            return new CreateCategoryResponse(createdCategory, createdCategory != null);
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
            var existingCategory = await _categoryRepository.GetCategoryById(request.CategoryId);
            if (existingCategory == null)
            {
                return new UpdateCategoryResponse(null, false);
            }

            // Update the category
            existingCategory.Name = request.Name.Trim();

            // Save via repository
            var updatedCategory = await _categoryRepository.UpdateCategory(existingCategory);
            return new UpdateCategoryResponse(updatedCategory, updatedCategory != null);
        }
    }

    public class ArchiveCategoryHandler : IArchiveCategoryHandler
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITopicRepository _topicRepository;

        public ArchiveCategoryHandler(ICategoryRepository categoryRepository, ITopicRepository topicRepository)
        {
            _categoryRepository = categoryRepository;
            _topicRepository = topicRepository;
        }

        public async Task<ArchiveCategoryResponse> Handle(ArchiveCategoryRequest request)
        {
            // Get the existing category
            var existingCategory = await _categoryRepository.GetCategoryById(request.CategoryId);
            if (existingCategory == null)
            {
                return new ArchiveCategoryResponse(null, false);
            }

            // Business logic: Archive the category by setting IsActive to false
            existingCategory.IsActive = false;

            // Archive all topics in this category
            var topicsInCategory = await _topicRepository.GetTopicsByCategoryId(request.CategoryId);
            foreach (var topic in topicsInCategory)
            {
                if (topic.IsActive)
                {
                    topic.IsActive = false;
                    await _topicRepository.UpdateTopic(topic);
                }
            }

            // Save the category via repository
            var archivedCategory = await _categoryRepository.UpdateCategory(existingCategory);
            return new ArchiveCategoryResponse(archivedCategory, archivedCategory != null);
        }
    }
}
