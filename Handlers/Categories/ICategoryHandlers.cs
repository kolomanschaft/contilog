using Contilog.Handlers;

namespace Contilog.Handlers.Categories
{
    public interface IGetAllCategoriesHandler : IHandler<GetAllCategoriesRequest, GetAllCategoriesResponse>
    {
    }
    
    public interface IUpdateCategoryHandler : IHandler<UpdateCategoryRequest, UpdateCategoryResponse>
    {
    }
    
    public interface IArchiveCategoryHandler : IHandler<ArchiveCategoryRequest, ArchiveCategoryResponse>
    {
    }
}
