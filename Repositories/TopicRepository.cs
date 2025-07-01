using Contilog.Models;

namespace Contilog.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly List<Topic> _topics;
        public TopicRepository()
        {
            _topics = new List<Topic>
            {
                new Topic { Id = 1, Title = "Getting Started with Blazor", CategoryId = 1, Author = "John Doe", CreatedDate = DateTime.Now.AddDays(-30), IsActive = true },
                new Topic { Id = 2, Title = "ASP.NET Core Best Practices", CategoryId = 2, Author = "Jane Smith", CreatedDate = DateTime.Now.AddDays(-15), IsActive = true },
                new Topic { Id = 3, Title = "Entity Framework Tips", CategoryId = 3, Author = "Mike Johnson", CreatedDate = DateTime.Now.AddDays(-7), IsActive = true },
                new Topic { Id = 4, Title = "CSS Grid Layout", CategoryId = 4, Author = "Sarah Wilson", CreatedDate = DateTime.Now.AddDays(-45), IsActive = false },
                new Topic { Id = 5, Title = "JavaScript ES6 Features", CategoryId = 4, Author = "Alex Brown", CreatedDate = DateTime.Now.AddDays(-22), IsActive = true },
                new Topic { Id = 6, Title = "Docker for Developers", CategoryId = 5, Author = "Chris Davis", CreatedDate = DateTime.Now.AddDays(-3), IsActive = true },
                new Topic { Id = 7, Title = "Testing Strategies", CategoryId = 6, Author = "Emma Miller", CreatedDate = DateTime.Now.AddDays(-60), IsActive = false },
                new Topic { Id = 8, Title = "Performance Optimization", CategoryId = 2, Author = "David Lee", CreatedDate = DateTime.Now.AddDays(-12), IsActive = true }
            };
        }

        public async Task<IEnumerable<Topic>> GetAllTopics()
        {
            return await Task.FromResult(_topics);
        }

        public async Task<Topic?> GetTopicById(int id)
        {
            return await Task.FromResult(_topics.FirstOrDefault(t => t.Id == id));
        }

        public async Task<IEnumerable<Topic>> GetTopicsByCategoryId(int categoryId)
        {
            return await Task.FromResult(_topics.Where(t => t.CategoryId == categoryId));
        }

        public Task<bool> DeleteTopic(int id)
        {
            var topic = _topics.FirstOrDefault(t => t.Id == id);
            if (topic == null)
                return Task.FromResult(false);

            _topics.Remove(topic);
            return Task.FromResult(true);
        }

        public Task<Topic?> CreateTopic(Topic topic)
        {
            // Generate new ID
            var nextId = _topics.Any() ? _topics.Max(t => t.Id) + 1 : 1;
            
            var newTopic = new Topic
            {
                Id = nextId,
                Title = topic.Title,
                CategoryId = topic.CategoryId,
                Author = topic.Author,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true
            };
            
            _topics.Add(newTopic);
            return Task.FromResult<Topic?>(newTopic);
        }

        public Task<Topic?> UpdateTopic(Topic topic)
        {
            var existingTopic = _topics.FirstOrDefault(t => t.Id == topic.Id);
            if (existingTopic == null)
            {
                return Task.FromResult<Topic?>(null);
            }

            // Update the existing topic
            existingTopic.Title = topic.Title;
            existingTopic.CategoryId = topic.CategoryId;
            existingTopic.Author = topic.Author;
            existingTopic.IsActive = topic.IsActive;
            existingTopic.ModifiedDate = DateTime.Now;

            return Task.FromResult<Topic?>(existingTopic);
        }
    }
}
