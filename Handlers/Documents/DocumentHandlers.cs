using Contilog.Repositories;

namespace Contilog.Handlers.Documents
{
    public class GetAllDocumentsHandler : IGetAllDocumentsHandler
    {
        private readonly IDocumentRepository _documentRepository;

        public GetAllDocumentsHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<GetAllDocumentsResponse> Handle(GetAllDocumentsRequest request)
        {
            var documents = await _documentRepository.GetAllAsync();
            return new GetAllDocumentsResponse(documents);
        }
    }

    public class GetDocumentByIdHandler : IGetDocumentByIdHandler
    {
        private readonly IDocumentRepository _documentRepository;

        public GetDocumentByIdHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<GetDocumentByIdResponse> Handle(GetDocumentByIdRequest request)
        {
            var document = await _documentRepository.GetByIdAsync(request.Id);
            return new GetDocumentByIdResponse(document);
        }
    }

    public class CreateDocumentHandler : ICreateDocumentHandler
    {
        private readonly IDocumentRepository _documentRepository;

        public CreateDocumentHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<CreateDocumentResponse> Handle(CreateDocumentRequest request)
        {
            var document = await _documentRepository.CreateAsync(request.Document);
            return new CreateDocumentResponse(document);
        }
    }

    public class DeleteDocumentHandler : IDeleteDocumentHandler
    {
        private readonly IDocumentRepository _documentRepository;

        public DeleteDocumentHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<DeleteDocumentResponse> Handle(DeleteDocumentRequest request)
        {
            var success = await _documentRepository.DeleteAsync(request.Id);
            return new DeleteDocumentResponse(success);
        }
    }
}
