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

        public async Task<GetAllTopicsResponse> HandleAsync(GetAllTopicsRequest request)
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

        public async Task<GetTopicByIdResponse> HandleAsync(GetTopicByIdRequest request)
        {
            var topic = await _topicRepository.GetTopicByIdAsync(request.TopicId);
            return new GetTopicByIdResponse(topic);
        }
    }

    public class CreateTopicHandler : ICreateTopicHandler
    {
        private readonly ITopicRepository _topicRepository;

        public CreateTopicHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<CreateTopicResponse> HandleAsync(CreateTopicRequest request)
        {
            // Business logic: validate the request
            if (string.IsNullOrWhiteSpace(request.Title))
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

        public DeleteTopicHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<DeleteTopicResponse> HandleAsync(DeleteTopicRequest request)
        {
            // Business logic: could add validation here (e.g., check permissions, soft delete, etc.)
            var success = await _topicRepository.DeleteTopicAsync(request.TopicId);
            return new DeleteTopicResponse(success);
        }
    }
}
