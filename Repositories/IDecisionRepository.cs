using Contilog.Models;

namespace Contilog.Repositories
{
    public interface IDecisionRepository
    {
        Task<IEnumerable<Decision>> GetDecisionsByPostId(int postId);
        Task<Decision?> GetDecisionById(int id);
        Task<Decision?> CreateDecision(Decision decision);
        Task<Decision?> UpdateDecision(Decision decision);
        Task<bool> DeleteDecision(int id);
    }
}
