using Contilog.Models;

namespace Contilog.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly List<Document> _documents = new();
        private int _nextId = 1;

        public Task<Document> CreateAsync(Document document)
        {
            document.Id = _nextId++;
            document.CreatedDate = DateTime.Now;
            _documents.Add(document);
            return Task.FromResult(document);
        }

        public Task<Document?> GetByIdAsync(int id)
        {
            var document = _documents.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(document);
        }

        public Task<IEnumerable<Document>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Document>>(_documents);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var document = _documents.FirstOrDefault(d => d.Id == id);
            if (document != null)
            {
                _documents.Remove(document);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
