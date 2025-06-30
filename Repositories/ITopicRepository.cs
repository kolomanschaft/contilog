using Contilog.Models;

namespace Contilog.Repositories
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topic>> GetAllTopicsAsync();
        Task<Topic?> GetTopicByIdAsync(int id);
        Task<IEnumerable<Topic>> GetTopicsByCategoryIdAsync(int categoryId);
        Task<bool> DeleteTopicAsync(int id);
        Task<Topic?> CreateTopicAsync(Topic topic);
        Task<Topic?> UpdateTopicAsync(Topic topic);
    }
}
