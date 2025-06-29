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
                new Post { Id = 1, TopicId = 1, Content = "This is an introduction to C# programming. We'll cover the basics of syntax, variables, and control structures.", Author = "John Smith", CreatedDate = DateTime.Now.AddDays(-5) },
                new Post { Id = 2, TopicId = 1, Content = "Let's explore some advanced C# features like generics, LINQ, and async/await patterns.", Author = "Jane Doe", CreatedDate = DateTime.Now.AddDays(-3) },
                new Post { Id = 3, TopicId = 1, Content = "Here are some tips to improve C# performance: use StringBuilder for string concatenation, prefer foreach over for loops when possible, and consider using value types.", Author = "Mike Johnson", CreatedDate = DateTime.Now.AddDays(-1) },
                
                new Post { Id = 4, TopicId = 2, Content = "Blazor is a framework for building web apps using C# instead of JavaScript. It comes in two flavors: Server and WebAssembly.", Author = "Sarah Wilson", CreatedDate = DateTime.Now.AddDays(-4) },
                new Post { Id = 5, TopicId = 2, Content = "Learn how components communicate in Blazor using parameters, event callbacks, and cascading values.", Author = "Tom Brown", CreatedDate = DateTime.Now.AddDays(-2) },
                
                new Post { Id = 6, TopicId = 3, Content = "Follow these best practices for database design: normalize your data, use appropriate data types, and establish proper relationships.", Author = "Lisa Chen", CreatedDate = DateTime.Now.AddDays(-6) },
                new Post { Id = 7, TopicId = 3, Content = "Optimize your SQL queries with these techniques: use proper indexing, avoid SELECT *, and consider query execution plans.", Author = "David Lee", CreatedDate = DateTime.Now.AddDays(-4) },
                new Post { Id = 8, TopicId = 3, Content = "Understanding when to use NoSQL vs SQL databases depends on your data structure, scalability needs, and consistency requirements.", Author = "Amy Taylor", CreatedDate = DateTime.Now.AddDays(-2) },
                
                new Post { Id = 9, TopicId = 4, Content = "Understanding React hooks and their usage - useState for state management, useEffect for side effects, and custom hooks for reusable logic.", Author = "Chris Anderson", CreatedDate = DateTime.Now.AddDays(-5) },
                new Post { Id = 10, TopicId = 4, Content = "Managing application state in React applications using Context API, Redux, or Zustand for complex state management scenarios.", Author = "Emily Davis", CreatedDate = DateTime.Now.AddDays(-3) },
                
                new Post { Id = 11, TopicId = 5, Content = "Start your journey with Python programming. Learn about variables, data types, functions, and basic syntax in this beginner-friendly guide.", Author = "Robert Kim", CreatedDate = DateTime.Now.AddDays(-7) },
                new Post { Id = 12, TopicId = 5, Content = "Essential libraries for data science in Python include NumPy for numerical computing, Pandas for data manipulation, and Matplotlib for visualization.", Author = "Maria Garcia", CreatedDate = DateTime.Now.AddDays(-5) },
                new Post { Id = 13, TopicId = 5, Content = "Comparing Django and Flask web frameworks - Django is full-featured and opinionated, while Flask is lightweight and flexible.", Author = "Alex Rodriguez", CreatedDate = DateTime.Now.AddDays(-3) },
                
                new Post { Id = 14, TopicId = 6, Content = "Setting up continuous integration and deployment pipelines to automate your software delivery process and improve code quality.", Author = "Jennifer White", CreatedDate = DateTime.Now.AddDays(-4) },
                new Post { Id = 15, TopicId = 6, Content = "Containerizing applications with Docker allows for consistent deployment across different environments and simplifies dependency management.", Author = "Kevin Zhang", CreatedDate = DateTime.Now.AddDays(-2) },
                
                new Post { Id = 16, TopicId = 7, Content = "Securing your cloud infrastructure requires proper access controls, network security, encryption, and regular security audits.", Author = "Rachel Green", CreatedDate = DateTime.Now.AddDays(-6) },
                new Post { Id = 17, TopicId = 7, Content = "Implementing secure authentication and authorization in web applications using JWT tokens, OAuth, and proper session management.", Author = "Daniel Park", CreatedDate = DateTime.Now.AddDays(-4) },
                
                new Post { Id = 18, TopicId = 8, Content = "Introduction to machine learning concepts including supervised vs unsupervised learning, training data, and model evaluation.", Author = "Sophie Miller", CreatedDate = DateTime.Now.AddDays(-8) },
                new Post { Id = 19, TopicId = 8, Content = "Understanding how neural networks work - from perceptrons to deep learning architectures and backpropagation algorithms.", Author = "Mark Thompson", CreatedDate = DateTime.Now.AddDays(-6) },
                new Post { Id = 20, TopicId = 8, Content = "Addressing ethical considerations in AI development including bias detection, fairness, transparency, and responsible AI practices.", Author = "Laura Johnson", CreatedDate = DateTime.Now.AddDays(-4) }
            };
        }

        public async Task<IEnumerable<Post>> GetPostsByTopicIdAsync(int topicId)
        {
            var posts = _posts.Where(p => p.TopicId == topicId).OrderByDescending(p => p.CreatedDate).AsEnumerable();
            return await Task.FromResult(posts);
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(post);
        }

        public Task<int> GetPostCountByTopicIdAsync(int topicId)
        {
            var count = _posts.Count(p => p.TopicId == topicId);
            return Task.FromResult(count);
        }

        public Task<Post?> UpdatePostAsync(Post post)
        {
            var existingPost = _posts.FirstOrDefault(p => p.Id == post.Id);
            if (existingPost != null)
            {
                existingPost.Content = post.Content;
                existingPost.ModifiedDate = DateTime.Now;
                // Note: We don't update Author or CreatedDate for existing posts
                return Task.FromResult<Post?>(existingPost);
            }
            return Task.FromResult<Post?>(null);
        }
    }
}
