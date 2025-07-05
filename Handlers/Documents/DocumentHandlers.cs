using System.Threading.Tasks;
using Contilog.Models;
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
        private readonly IPostRepository _postRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDecisionRepository _decisionRepository;

        public CreateDocumentHandler(
            IDocumentRepository documentRepository,
            IPostRepository postRepository,
            ITopicRepository topicRepository,
            ICategoryRepository categoryRepository,
            IDecisionRepository decisionRepository)
        {
            _documentRepository = documentRepository;
            _postRepository = postRepository;
            _topicRepository = topicRepository;
            _categoryRepository = categoryRepository;
            _decisionRepository = decisionRepository;
        }

        public async Task<CreateDocumentResponse> Handle(CreateDocumentRequest request)
        {
            var documentPosts = await ToDocumentPosts(await _postRepository.GetAllPosts());
            var document = new Document
            {
                Title = request.Title,
                DocumentDate = request.documentDate,
                Attendees = request.Attendees,
                Posts = documentPosts.ToList()
            };
            var createdDocument = await _documentRepository.CreateAsync(document);
            return new CreateDocumentResponse(createdDocument, createdDocument != null);
        }

        private async Task<IEnumerable<DocumentPost>> ToDocumentPosts(IEnumerable<Post> posts)
        {
            var allCategories = await _categoryRepository.GetAllCategories();
            var categoryIdMap = allCategories.ToDictionary(c => c.Id, c => c);
            var allTopics = await _topicRepository.GetAllTopics();
            var topicIdMap = allTopics.ToDictionary(t => t.Id, t => t);

            return posts.Select(p =>
            {
                if (!topicIdMap.TryGetValue(p.TopicId, out var topic))
                {
                    throw new KeyNotFoundException($"Topic with ID {p.TopicId} not found.");
                }
                if (!categoryIdMap.TryGetValue(topic.CategoryId, out var category))
                {
                    throw new KeyNotFoundException($"Category with ID {topic.CategoryId} not found for topic {topic.Id}.");
                }
                return new DocumentPost
                {
                    PostId = p.Id,
                    PostContent = p.Content,
                    TopicTitle = topic.Title,
                    CategoryName = category.Name
                };
            });
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
