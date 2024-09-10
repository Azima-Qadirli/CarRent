using CarRent.Views.Models.BaseEntities;

namespace CarRent.Views.Models;

public class Ban:BaseEntity
{
    public string Name { get; set; }
    public ICollection<Car>? Cars { get; set; } = new List<Car>();
}