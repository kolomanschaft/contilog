namespace Contilog.Models;

public class Topic
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string Author { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
