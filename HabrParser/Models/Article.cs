using System.ComponentModel.DataAnnotations.Schema;

namespace HabrParser.Models;

public class Article
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Creator { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public DateTime PublishedAt { get; set; }

    public string Link { get; set; }

    public string? ImageLink { get; set; }
}