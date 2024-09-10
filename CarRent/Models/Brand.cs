using CarRent.Views.Models.BaseEntities;

namespace CarRent.Views.Models;

public class Brand:BaseEntity
{
    public string Name { get; set; }
    public List<Model> Models { get; set; } = new List<Model>();
}