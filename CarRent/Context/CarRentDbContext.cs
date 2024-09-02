using CarRent.Views.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Context;

public class CarRentDbContext:DbContext
{
    public CarRentDbContext(DbContextOptions<CarRentDbContext>options):base(options)
    {
        
    }

    public DbSet<Service>Services { get; set; }
    public DbSet<Staff>Staves { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BlogTag> BlogTags { get; set; }
    public DbSet<Carousel>Carousels { get; set; }
}