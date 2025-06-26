namespace Contilog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int TopicId { get; set; }
    }
}
