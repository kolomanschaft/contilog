using Contilog.Models;

namespace Contilog.Handlers.Categories
{
    // Requests
    public record GetAllCategoriesRequest();
    
    public record GetCategoryByIdRequest(int CategoryId);
    
    public record CreateCategoryRequest(string Name);
    
    public record UpdateCategoryRequest(int CategoryId, string Name);
    
    public record ArchiveCategoryRequest(int CategoryId);

    // Responses
    public record GetAllCategoriesResponse(IEnumerable<Category> Categories);
    
    public record GetCategoryByIdResponse(Category? Category);
    
    public record CreateCategoryResponse(Category? Category, bool Success);
    
    public record UpdateCategoryResponse(Category? Category, bool Success);
    
    public record ArchiveCategoryResponse(Category? Category, bool Success);
}
