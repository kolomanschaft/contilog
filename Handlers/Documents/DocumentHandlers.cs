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

    public class GenerateTxtDocumentHandler : IGenerateTxtDocumentHandler
    {
        public Task<GenerateTxtDocumentResponse> Handle(GenerateTxtDocumentRequest request)
        {
            try
            {
                var textContent = GenerateTextContent(request.Document);
                var bytes = System.Text.Encoding.UTF8.GetBytes(textContent);
                var fileName = $"{request.Document.Title}_{request.Document.DocumentDate:yyyy-MM-dd}.txt";
                var base64 = Convert.ToBase64String(bytes);
                return Task.FromResult(new GenerateTxtDocumentResponse(fileName, base64, "text/plain", true));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating document: {ex.GetType().Name}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                }
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            return Task.FromResult(new GenerateTxtDocumentResponse(string.Empty, string.Empty, string.Empty, false));
        }

        private string GenerateTextContent(Document document)
        {
            var content = new System.Text.StringBuilder();
            
            content.AppendLine($"Document: {document.Title}");
            content.AppendLine($"Date: {document.DocumentDate:MMMM dd, yyyy}");
            content.AppendLine($"Created: {document.CreatedDate:MMMM dd, yyyy HH:mm}");
            content.AppendLine();
            
            if (document.Attendees.Any())
            {
                content.AppendLine("Attendees:");
                foreach (var attendee in document.Attendees)
                {
                    content.AppendLine($"  • {attendee}");
                }
                content.AppendLine();
            }
            
            content.AppendLine($"Posts ({document.Posts.Count} total)");
            content.AppendLine(new string('=', 50));
            
            var postsByTopic = document.Posts.GroupBy(p => p.TopicTitle).OrderBy(g => g.Key);
            
            foreach (var topicGroup in postsByTopic)
            {
                content.AppendLine();
                content.AppendLine($"Topic: {topicGroup.Key}");
                content.AppendLine(new string('-', 30));
                
                foreach (var post in topicGroup.OrderBy(p => p.PostContent))
                {
                    content.AppendLine();
                    content.AppendLine(StripHtmlTags(post.PostContent));
                    content.AppendLine($"Category: {post.CategoryName} | Post ID: {post.PostId}");
                    content.AppendLine();
                }
            }
            
            return content.ToString();
        }

        private string StripHtmlTags(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}
