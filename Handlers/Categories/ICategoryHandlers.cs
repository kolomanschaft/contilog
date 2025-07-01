using Contilog.Handlers;

namespace Contilog.Handlers.Categories
{
    public interface IGetAllCategoriesHandler : IHandler<GetAllCategoriesRequest, GetAllCategoriesResponse>
    {
    }
    
    public interface IGetCategoryByIdHandler : IHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
    {
    }
    
    public interface ICreateCategoryHandler : IHandler<CreateCategoryRequest, CreateCategoryResponse>
    {
    }
    
    public interface IUpdateCategoryHandler : IHandler<UpdateCategoryRequest, UpdateCategoryResponse>
    {
    }
    
    public interface IArchiveCategoryHandler : IHandler<ArchiveCategoryRequest, ArchiveCategoryResponse>
    {
    }
}
