using Contilog.Models;
using Contilog.Repositories;

namespace Contilog.Handlers.Topics
{
    public class GetAllTopicsHandler : IGetAllTopicsHandler
    {
        private readonly ITopicRepository _topicRepository;

        public GetAllTopicsHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<GetAllTopicsResponse> Handle(GetAllTopicsRequest request)
        {
            var topics = await _topicRepository.GetAllTopicsAsync();
            return new GetAllTopicsResponse(topics);
        }
    }

    public class GetTopicByIdHandler : IGetTopicByIdHandler
    {
        private readonly ITopicRepository _topicRepository;

        public GetTopicByIdHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<GetTopicByIdResponse> Handle(GetTopicByIdRequest request)
        {
            var topic = await _topicRepository.GetTopicByIdAsync(request.TopicId);
            return new GetTopicByIdResponse(topic);
        }
    }

    public class CreateTopicHandler : ICreateTopicHandler
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateTopicHandler(ITopicRepository topicRepository, ICategoryRepository categoryRepository)
        {
            _topicRepository = topicRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateTopicResponse> Handle(CreateTopicRequest request)
        {
            // Business logic: validate the request
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return new CreateTopicResponse(null, false);
            }

            // Validate that the category exists
            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            if (category == null)
            {
                return new CreateTopicResponse(null, false);
            }

            // Create the topic entity
            var topic = new Topic
            {
                Title = request.Title.Trim(),
                CategoryId = request.CategoryId,
                Author = request.Author,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            // Save via repository
            var createdTopic = await _topicRepository.CreateTopicAsync(topic);
            return new CreateTopicResponse(createdTopic, createdTopic != null);
        }
    }

    public class DeleteTopicHandler : IDeleteTopicHandler
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;

        public DeleteTopicHandler(ITopicRepository topicRepository, IPostRepository postRepository)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
        }

        public async Task<DeleteTopicResponse> Handle(DeleteTopicRequest request)
        {
            // Business logic: Delete all posts associated with the topic first
            var posts = await _postRepository.GetPostsByTopicIdAsync(request.TopicId);
            foreach (var post in posts)
            {
                await _postRepository.DeletePostAsync(post.Id);
            }

            // Then delete the topic itself
            var success = await _topicRepository.DeleteTopicAsync(request.TopicId);
            return new DeleteTopicResponse(success);
        }
    }
}
