using Contilog.Models;

namespace Contilog.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsByTopicIdAsync(int topicId);
        Task<Post?> GetPostByIdAsync(int id);
        Task<int> GetPostCountByTopicIdAsync(int topicId);
        Task<Post?> UpdatePostAsync(Post post);
    }
}
