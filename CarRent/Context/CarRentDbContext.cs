using CarRent.Views.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Context;

public class CarRentDbContext:IdentityDbContext<AppUser>
{
    public CarRentDbContext(DbContextOptions<CarRentDbContext>options):base(options)
    {
        
    }

    public DbSet<Service>Services { get; set; }
    public DbSet<Staff>Staves { get; set; }
    // public DbSet<Blog> Blogs { get; set; }
    // public DbSet<Tag> Tags { get; set; }
    // public DbSet<Category> Categories { get; set; }
    // public DbSet<BlogTag> BlogTags { get; set; }
    public DbSet<Blog>Blogs { get; set; }
    public DbSet<Tag>Tags { get; set; }
    public DbSet<Category>Categories { get; set;}
    public DbSet<BlogTag>BlogTags { get; set; }
    public DbSet<Carousel>Carousels { get; set; }
    public DbSet<Message>Messages { get; set; }
    public DbSet<Car>Cars { get; set; }
    public DbSet<Brand>Brands { get; set; }
    public DbSet<Model>Models { get; set; }
    public DbSet<Setting>Settings { get; set; }
    public DbSet<Ban>Bans { get; set; }
}