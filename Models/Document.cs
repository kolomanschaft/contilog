namespace Contilog.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string JsonContent { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
