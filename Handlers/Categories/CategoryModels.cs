using Contilog.Models;

namespace Contilog.Handlers.Categories
{
    // Requests
    public record GetAllCategoriesRequest();
    
    public record GetCategoryByIdRequest(int CategoryId);
    
    public record UpdateCategoryRequest(int CategoryId, string Name);

    // Responses
    public record GetAllCategoriesResponse(IEnumerable<Category> Categories);
    
    public record GetCategoryByIdResponse(Category? Category);
    
    public record UpdateCategoryResponse(Category? Category, bool Success);
}
