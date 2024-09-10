using CarRent.Views.Models.BaseEntities;

namespace CarRent.Views.Models;

public class Setting:BaseEntity
{
    public int HappyClientCount { get; set; }
    public int NumberOfCars { get; set; }
    public int CarCenter  { get; set; }
    public int TotalKilometers { get; set; }
    public string LocationLink{ get; set; }
    public string PhoneNumber1 { get; set; }
    public string PhoneNumber2 { get; set; }
    public string Email { get; set; }
}