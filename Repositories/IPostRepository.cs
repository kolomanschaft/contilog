using Contilog.Models;

namespace Contilog.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsByTopicId(int topicId);
        Task<Post?> GetPostById(int id);
        Task<int> GetPostCountByTopicId(int topicId);
        Task<Post?> UpdatePost(Post post);
        Task<Post?> CreatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}
