using Contilog.Models;

namespace Contilog.Handlers.Posts
{
    // Requests
    public record GetPostsByTopicIdRequest(int TopicId);
    
    public record GetPostByIdRequest(int PostId);
    
    public record GetPostCountByTopicIdRequest(int TopicId);
    
    public record CreatePostRequest(string Content, int TopicId, string Author);
    
    public record UpdatePostRequest(int PostId, string Content);
    
    public record DeletePostRequest(int PostId);

    // Responses
    public record GetPostsByTopicIdResponse(IEnumerable<Post> Posts);
    
    public record GetPostByIdResponse(Post? Post);
    
    public record GetPostCountByTopicIdResponse(int Count);
    
    public record CreatePostResponse(Post? Post, bool Success);
    
    public record UpdatePostResponse(Post? Post, bool Success);
    
    public record DeletePostResponse(bool Success);
}
