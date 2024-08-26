using CarRent.Views.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Context;

public class CarRentDbContext:DbContext
{
    public CarRentDbContext(DbContextOptions<CarRentDbContext>options):base(options)
    {
        
    }

    public DbSet<Service>Services { get; set; }
}