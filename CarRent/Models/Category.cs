using CarRent.Views.Models.BaseEntities;

namespace CarRent.Views.Models;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public ICollection<Blog>? Blogs { get; set; }
}