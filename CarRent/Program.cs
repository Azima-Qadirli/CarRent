using CarRent.Context;
using CarRent.Repositories;
using CarRent.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CarRentDbContext>(opt =>
{
    opt.UseSqlServer("Server=DESKTOP-UM6TF1M;Database=CarRent;Integrated Security = true;Encrypt=false;");
});


builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapAreaControllerRoute(
        name: "admin",
        pattern: "admin/{controller=Home}/{action=Index}/{id?}",
        areaName: "admin"
    );

    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});



// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();