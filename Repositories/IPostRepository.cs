using Contilog.Models;

namespace Contilog.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsByTopicIdAsync(int topicId);
        Task<int> GetPostCountByTopicIdAsync(int topicId);
    }
}
