namespace Contilog.Models
{
    public class Decision
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
