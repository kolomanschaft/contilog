using Contilog.Handlers;

namespace Contilog.Handlers.Documents
{
    public interface IGetAllDocumentsHandler : IHandler<GetAllDocumentsRequest, GetAllDocumentsResponse>
    {
    }

    public interface IGetDocumentByIdHandler : IHandler<GetDocumentByIdRequest, GetDocumentByIdResponse>
    {
    }

    public interface ICreateDocumentHandler : IHandler<CreateDocumentRequest, CreateDocumentResponse>
    {
    }

    public interface IDeleteDocumentHandler : IHandler<DeleteDocumentRequest, DeleteDocumentResponse>
    {
    }

    public interface IGenerateTxtDocumentHandler : IHandler<GenerateTxtDocumentRequest, GenerateTxtDocumentResponse>
    {
    }
}
