using Contilog.Models;

namespace Contilog.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document> CreateAsync(Document document);
        Task<Document?> GetByIdAsync(int id);
        Task<IEnumerable<Document>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
