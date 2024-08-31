using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Controllers;

public class ServiceController : Controller
{
    private readonly IRepository<Service> _repository;

    public ServiceController(IRepository<Service> repository)
    {
        
        _repository = repository;
    }

    public async  Task<IActionResult> Index()
    {
        var response = await _repository.GetAll().ToListAsync();
        return View(response);
    }
}