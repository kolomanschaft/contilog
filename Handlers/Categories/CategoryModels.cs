using Contilog.Models;

namespace Contilog.Handlers.Categories
{
    // Requests
    public record GetAllCategoriesRequest();
    
    public record GetCategoryByIdRequest(int CategoryId);

    // Responses
    public record GetAllCategoriesResponse(IEnumerable<Category> Categories);
    
    public record GetCategoryByIdResponse(Category? Category);
}
