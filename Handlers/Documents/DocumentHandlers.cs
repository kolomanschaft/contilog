using System.Threading.Tasks;
using Contilog.Models;
using Contilog.Repositories;
using PdfSharp.Pdf;
using PdfSharp.Drawing;


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

    public class GeneratePdfDocumentHandler : IGeneratePdfDocumentHandler
    {
        public Task<GeneratePdfDocumentResponse> Handle(GeneratePdfDocumentRequest request)
        {
            try
            {
                var pdfBytes = GeneratePdfContent(request.Document);
                var fileName = $"{request.Document.Title}_{request.Document.DocumentDate:yyyy-MM-dd}.pdf";
                var base64 = Convert.ToBase64String(pdfBytes);
                return Task.FromResult(new GeneratePdfDocumentResponse(fileName, base64, "application/pdf", true));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating PDF document: {ex.GetType().Name}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                }
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            return Task.FromResult(new GeneratePdfDocumentResponse(string.Empty, string.Empty, string.Empty, false));
        }

        private byte[] GeneratePdfContent(Document document)
        {
            // Register the CodePages encoding provider for PDFSharp compatibility with .NET Core/5+
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            
            // Create a new PDF document
            var pdf = new PdfDocument();
            pdf.Info.Title = document.Title;
            pdf.Info.Author = "Contilog";
            pdf.Info.Subject = $"Document from {document.DocumentDate:MMMM dd, yyyy}";

            // Create the first page
            var page = pdf.AddPage();
            var graphics = XGraphics.FromPdfPage(page);
            
            // Use fonts that work with PDFSharp 1.x
            var titleFont = new XFont("Courier New", 16, XFontStyle.Bold);
            var headerFont = new XFont("Courier New", 12, XFontStyle.Bold);
            var bodyFont = new XFont("Courier New", 10, XFontStyle.Regular);
            var smallFont = new XFont("Courier New", 8, XFontStyle.Regular);
            
            // Define colors
            var titleBrush = XBrushes.DarkBlue;
            var headerBrush = XBrushes.Black;
            var bodyBrush = XBrushes.Black;
            var lightGrayBrush = XBrushes.Gray;
            
            // Current Y position
            double yPosition = 60;
            var leftMargin = 60.0;
            var rightMargin = page.Width - 60.0;
            var pageWidth = rightMargin - leftMargin;
            
            // Title
            graphics.DrawString(document.Title, titleFont, titleBrush, leftMargin, yPosition);
            yPosition += 40;
            
            // Document date
            graphics.DrawString($"Date: {document.DocumentDate:MMMM dd, yyyy}", bodyFont, bodyBrush, leftMargin, yPosition);
            yPosition += 20;
            
            // Created date
            graphics.DrawString($"Created: {document.CreatedDate:MMMM dd, yyyy HH:mm}", bodyFont, bodyBrush, leftMargin, yPosition);
            yPosition += 30;
            
            // Attendees
            if (document.Attendees.Any())
            {
                graphics.DrawString("Attendees:", headerFont, headerBrush, leftMargin, yPosition);
                yPosition += 20;
                
                foreach (var attendee in document.Attendees)
                {
                    graphics.DrawString($"• {attendee}", bodyFont, bodyBrush, leftMargin + 20, yPosition);
                    yPosition += 18;
                }
                yPosition += 20;
            }
            
            // Posts section
            graphics.DrawString($"Posts ({document.Posts.Count} total)", headerFont, headerBrush, leftMargin, yPosition);
            yPosition += 30;
            
            // Draw a line separator
            graphics.DrawLine(XPens.Gray, leftMargin, yPosition, rightMargin, yPosition);
            yPosition += 20;
            
            // Group posts by topic
            var postsByTopic = document.Posts.GroupBy(p => p.TopicTitle).OrderBy(g => g.Key);
            
            foreach (var topicGroup in postsByTopic)
            {
                // Check if we need a new page
                if (yPosition > page.Height - 150)
                {
                    page = pdf.AddPage();
                    graphics = XGraphics.FromPdfPage(page);
                    yPosition = 60;
                }
                
                // Topic title
                graphics.DrawString($"Topic: {topicGroup.Key}", headerFont, headerBrush, leftMargin, yPosition);
                yPosition += 25;
                
                foreach (var post in topicGroup.OrderBy(p => p.PostContent))
                {
                    // Check if we need a new page for this post
                    if (yPosition > page.Height - 120)
                    {
                        page = pdf.AddPage();
                        graphics = XGraphics.FromPdfPage(page);
                        yPosition = 60;
                    }
                    
                    // Post content (strip HTML and wrap text)
                    var cleanContent = StripHtmlTags(post.PostContent);
                    yPosition = DrawWrappedText(graphics, cleanContent, bodyFont, bodyBrush, leftMargin + 20, yPosition, pageWidth - 20);
                    yPosition += 10;
                    
                    // Category and Post ID
                    graphics.DrawString($"Category: {post.CategoryName} | Post ID: {post.PostId}", smallFont, lightGrayBrush, leftMargin + 20, yPosition);
                    yPosition += 25;
                }
                
                yPosition += 10; // Extra space between topics
            }
            
            // Save the PDF to a memory stream
            using var stream = new MemoryStream();
            pdf.Save(stream);
            return stream.ToArray();
        }
        
        private double DrawWrappedText(XGraphics graphics, string text, XFont font, XBrush brush, double x, double y, double maxWidth)
        {
            var words = text.Split(' ');
            var currentLine = "";
            var lineHeight = font.GetHeight();
            var currentY = y;
            
            foreach (var word in words)
            {
                var testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                var size = graphics.MeasureString(testLine, font);
                
                if (size.Width > maxWidth && !string.IsNullOrEmpty(currentLine))
                {
                    // Draw the current line
                    graphics.DrawString(currentLine, font, brush, x, currentY);
                    currentY += lineHeight + 2;
                    currentLine = word;
                }
                else
                {
                    currentLine = testLine;
                }
            }
            
            // Draw the last line
            if (!string.IsNullOrEmpty(currentLine))
            {
                graphics.DrawString(currentLine, font, brush, x, currentY);
                currentY += lineHeight + 2;
            }
            
            return currentY;
        }
        
        private string StripHtmlTags(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}
