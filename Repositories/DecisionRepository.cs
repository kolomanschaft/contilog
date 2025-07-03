using Contilog.Models;

namespace Contilog.Repositories
{
    public class DecisionRepository : IDecisionRepository
    {
        private readonly List<Decision> _decisions;

        public DecisionRepository()
        {
            _decisions = new List<Decision>
            {
                new Decision { Id = 1, PostId = 1, Text = "We should proceed with implementing generics in our next code review session.", Author = "John Smith", CreatedDate = DateTime.Now.AddDays(-4), ModifiedDate = DateTime.Now.AddDays(-4) },
                new Decision { Id = 2, PostId = 2, Text = "Team agreed to adopt LINQ patterns in our upcoming projects.", Author = "Jane Doe", CreatedDate = DateTime.Now.AddDays(-2), ModifiedDate = DateTime.Now.AddDays(-1) },
                new Decision { Id = 3, PostId = 4, Text = "We will use Blazor Server for the company dashboard project.", Author = "Sarah Wilson", CreatedDate = DateTime.Now.AddDays(-3), ModifiedDate = DateTime.Now.AddDays(-3) },
                new Decision { Id = 4, PostId = 6, Text = "Database normalization will be implemented following the third normal form.", Author = "Lisa Chen", CreatedDate = DateTime.Now.AddDays(-5), ModifiedDate = DateTime.Now.AddDays(-4) },
                new Decision { Id = 5, PostId = 9, Text = "All React components should use hooks instead of class components going forward.", Author = "Chris Anderson", CreatedDate = DateTime.Now.AddDays(-4), ModifiedDate = DateTime.Now.AddDays(-4) }
            };
        }

        public async Task<IEnumerable<Decision>> GetDecisionsByPostId(int postId)
        {
            var decisions = _decisions.Where(d => d.PostId == postId).OrderByDescending(d => d.CreatedDate).AsEnumerable();
            return await Task.FromResult(decisions);
        }

        public async Task<Decision?> GetDecisionById(int id)
        {
            var decision = _decisions.FirstOrDefault(d => d.Id == id);
            return await Task.FromResult(decision);
        }

        public Task<Decision?> CreateDecision(Decision decision)
        {
            // Generate new ID
            var nextId = _decisions.Any() ? _decisions.Max(d => d.Id) + 1 : 1;
            
            var now = DateTime.UtcNow;
            var newDecision = new Decision
            {
                Id = nextId,
                PostId = decision.PostId,
                Text = decision.Text,
                Author = decision.Author,
                CreatedDate = now,
                ModifiedDate = now
            };
            
            _decisions.Add(newDecision);
            return Task.FromResult<Decision?>(newDecision);
        }

        public Task<Decision?> UpdateDecision(Decision decision)
        {
            var existingDecision = _decisions.FirstOrDefault(d => d.Id == decision.Id);
            if (existingDecision != null)
            {
                existingDecision.Text = decision.Text;
                existingDecision.ModifiedDate = DateTime.Now;
                // Note: We don't update Author, PostId, or CreatedDate for existing decisions
                return Task.FromResult<Decision?>(existingDecision);
            }
            return Task.FromResult<Decision?>(null);
        }

        public Task<bool> DeleteDecision(int id)
        {
            var decisionToDelete = _decisions.FirstOrDefault(d => d.Id == id);
            if (decisionToDelete != null)
            {
                _decisions.Remove(decisionToDelete);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
