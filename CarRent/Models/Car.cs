using CarRent.Views.Models.BaseEntities;
using CarRent.Views.Models.Enums;

namespace CarRent.Views.Models;

public class Car:BaseEntity
{
    public double Price { get; set; }
    public int NumberOfSeats { get; set; }
    public int BrandId { get; set; }
    public int BanId { get; set; }
    public int Mileage { get; set; }
    public DateTime FactoryDate { get; set; }
    public Petrol Petrol { get; set; }
    public GearBox GearBox { get; set; }
    public Ban Ban { get; set; }
    public Brand Brand { get; set; }
}