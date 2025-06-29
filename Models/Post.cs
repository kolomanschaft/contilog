namespace Contilog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
