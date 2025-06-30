using Contilog.Handlers;

namespace Contilog.Handlers.Categories
{
    public interface IGetAllCategoriesHandler : IHandler<GetAllCategoriesRequest, GetAllCategoriesResponse>
    {
    }

    public interface IGetCategoryByIdHandler : IHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
    {
    }
}
