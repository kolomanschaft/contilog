using Contilog.Models;

namespace Contilog.Handlers.Documents
{
    // Get all documents
    public record GetAllDocumentsRequest();
    public record GetAllDocumentsResponse(IEnumerable<Document> Documents);

    // Get document by ID
    public record GetDocumentByIdRequest(int Id);
    public record GetDocumentByIdResponse(Document? Document);

    // Create document
    public record CreateDocumentRequest(string Title, DateTime documentDate, List<string> Attendees);
    public record CreateDocumentResponse(Document Document, bool Success);

    // Delete document
    public record DeleteDocumentRequest(int Id);
    public record DeleteDocumentResponse(bool Success);
}
