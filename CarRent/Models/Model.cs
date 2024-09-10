using CarRent.Views.Models.BaseEntities;

namespace CarRent.Views.Models;

public class Model:BaseEntity   
{
    public string Name { get; set; }
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }
    public ICollection<Car>? Cars { get; set; }=new List<Car>();
}