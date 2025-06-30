using Contilog.Models;

namespace Contilog.Handlers.Topics
{
    // Requests
    public record GetAllTopicsRequest();
    
    public record GetTopicByIdRequest(int TopicId);
    
    public record CreateTopicRequest(string Title, int CategoryId, string Author);
    
    public record DeleteTopicRequest(int TopicId);

    // Responses
    public record GetAllTopicsResponse(IEnumerable<Topic> Topics);
    
    public record GetTopicByIdResponse(Topic? Topic);
    
    public record CreateTopicResponse(Topic? Topic, bool Success);
    
    public record DeleteTopicResponse(bool Success);
}
