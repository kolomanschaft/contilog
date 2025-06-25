namespace Contilog.Models;

public class Topic
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string Author { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
