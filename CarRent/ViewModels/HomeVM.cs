using CarRent.Views.Models;

namespace CarRent.ViewModels;

public class HomeVM
{
    public ICollection<Carousel> Carousels = new List<Carousel>();
    public ICollection<Staff> Staves = new List<Staff>();
}