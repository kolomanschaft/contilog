using Contilog.Models;

namespace Contilog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly List<Post> _posts;

        public PostRepository()
        {
            _posts = new List<Post>
            {
                new Post { Id = 1, TopicId = 1, Title = "Welcome to C# Development", Content = "This is an introduction to C# programming...", Author = "John Smith", CreatedDate = DateTime.Now.AddDays(-5) },
                new Post { Id = 2, TopicId = 1, Title = "Advanced C# Features", Content = "Let's explore some advanced C# features...", Author = "Jane Doe", CreatedDate = DateTime.Now.AddDays(-3) },
                new Post { Id = 3, TopicId = 1, Title = "C# Performance Tips", Content = "Here are some tips to improve C# performance...", Author = "Mike Johnson", CreatedDate = DateTime.Now.AddDays(-1) },
                
                new Post { Id = 4, TopicId = 2, Title = "Getting Started with Blazor", Content = "Blazor is a framework for building web apps...", Author = "Sarah Wilson", CreatedDate = DateTime.Now.AddDays(-4) },
                new Post { Id = 5, TopicId = 2, Title = "Blazor Component Communication", Content = "Learn how components communicate in Blazor...", Author = "Tom Brown", CreatedDate = DateTime.Now.AddDays(-2) },
                
                new Post { Id = 6, TopicId = 3, Title = "Database Design Best Practices", Content = "Follow these best practices for database design...", Author = "Lisa Chen", CreatedDate = DateTime.Now.AddDays(-6) },
                new Post { Id = 7, TopicId = 3, Title = "SQL Optimization Techniques", Content = "Optimize your SQL queries with these techniques...", Author = "David Lee", CreatedDate = DateTime.Now.AddDays(-4) },
                new Post { Id = 8, TopicId = 3, Title = "NoSQL vs SQL", Content = "Understanding when to use NoSQL vs SQL databases...", Author = "Amy Taylor", CreatedDate = DateTime.Now.AddDays(-2) },
                
                new Post { Id = 9, TopicId = 4, Title = "React Hooks Deep Dive", Content = "Understanding React hooks and their usage...", Author = "Chris Anderson", CreatedDate = DateTime.Now.AddDays(-5) },
                new Post { Id = 10, TopicId = 4, Title = "State Management in React", Content = "Managing application state in React applications...", Author = "Emily Davis", CreatedDate = DateTime.Now.AddDays(-3) },
                
                new Post { Id = 11, TopicId = 5, Title = "Python for Beginners", Content = "Start your journey with Python programming...", Author = "Robert Kim", CreatedDate = DateTime.Now.AddDays(-7) },
                new Post { Id = 12, TopicId = 5, Title = "Python Data Science Libraries", Content = "Essential libraries for data science in Python...", Author = "Maria Garcia", CreatedDate = DateTime.Now.AddDays(-5) },
                new Post { Id = 13, TopicId = 5, Title = "Django vs Flask", Content = "Comparing Django and Flask web frameworks...", Author = "Alex Rodriguez", CreatedDate = DateTime.Now.AddDays(-3) },
                
                new Post { Id = 14, TopicId = 6, Title = "CI/CD Pipeline Setup", Content = "Setting up continuous integration and deployment...", Author = "Jennifer White", CreatedDate = DateTime.Now.AddDays(-4) },
                new Post { Id = 15, TopicId = 6, Title = "Docker Containerization", Content = "Containerizing applications with Docker...", Author = "Kevin Zhang", CreatedDate = DateTime.Now.AddDays(-2) },
                
                new Post { Id = 16, TopicId = 7, Title = "Cloud Security Best Practices", Content = "Securing your cloud infrastructure...", Author = "Rachel Green", CreatedDate = DateTime.Now.AddDays(-6) },
                new Post { Id = 17, TopicId = 7, Title = "Authentication and Authorization", Content = "Implementing secure auth in web applications...", Author = "Daniel Park", CreatedDate = DateTime.Now.AddDays(-4) },
                
                new Post { Id = 18, TopicId = 8, Title = "Machine Learning Fundamentals", Content = "Introduction to machine learning concepts...", Author = "Sophie Miller", CreatedDate = DateTime.Now.AddDays(-8) },
                new Post { Id = 19, TopicId = 8, Title = "Neural Networks Explained", Content = "Understanding how neural networks work...", Author = "Mark Thompson", CreatedDate = DateTime.Now.AddDays(-6) },
                new Post { Id = 20, TopicId = 8, Title = "AI Ethics and Bias", Content = "Addressing ethical considerations in AI...", Author = "Laura Johnson", CreatedDate = DateTime.Now.AddDays(-4) }
            };
        }

        public Task<IEnumerable<Post>> GetPostsByTopicIdAsync(int topicId)
        {
            var posts = _posts.Where(p => p.TopicId == topicId).OrderByDescending(p => p.CreatedDate).AsEnumerable();
            return Task.FromResult(posts);
        }

        public Task<int> GetPostCountByTopicIdAsync(int topicId)
        {
            var count = _posts.Count(p => p.TopicId == topicId);
            return Task.FromResult(count);
        }
    }
}
