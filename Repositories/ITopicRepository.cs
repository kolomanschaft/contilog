using Contilog.Models;

namespace Contilog.Repositories
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topic>> GetAllTopics();
        Task<Topic?> GetTopicById(int id);
        Task<IEnumerable<Topic>> GetTopicsByCategoryId(int categoryId);
        Task<bool> DeleteTopic(int id);
        Task<Topic?> CreateTopic(Topic topic);
        Task<Topic?> UpdateTopic(Topic topic);
    }
}
