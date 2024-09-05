using System.ComponentModel.DataAnnotations.Schema;
using CarRent.Views.Models.BaseEntities;

namespace CarRent.Views.Models;

public class Author:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string FileName { get; set; }
    [NotMapped]
    public IFormFile File { get; set; }
    public ICollection<Blog>Blogs { get; set; }
}