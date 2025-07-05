namespace Contilog.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DocumentDate { get; set; }
        public List<string> Attendees { get; set; } = new();
        public List<DocumentPost> Posts { get; set; } = new();
        public DateTime CreatedDate { get; set; }
    }

    public class DocumentPost
    {
        public int PostId { get; set; }
        public string PostContent { get; set; } = string.Empty;
        public string TopicTitle { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public bool IsNew { get; set; } = false;
    }
}
