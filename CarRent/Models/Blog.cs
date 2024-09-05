using System.ComponentModel.DataAnnotations.Schema;
using CarRent.Views.Models.BaseEntities;

namespace CarRent.Views.Models;

public class Blog:BaseEntity
{
    public string Title1 { get; set; }
    public string Description1 { get; set; }
    public string Title2 { get; set; }
    public string Description2 { get; set; }
    
    public string FileName { get; set; }
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    public string Paragraph { get; set; }
    public Category? Category { get; set; }
    public Author? Author { get; set; }
    public ICollection<BlogTag>BlogTags { get; set; }
    [NotMapped]
    public IFormFile File { get; set; }
    [NotMapped]
    public int[] TagIDs { get; set; }
}