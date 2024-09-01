using System.Diagnostics;
using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarRent.Controllers;

public class HomeController : Controller
{

    private readonly IRepository<Staff> _repository;

    public HomeController(IRepository<Staff> repository)
    {
        _repository = repository;
    }

    public async  Task<ActionResult> Index()
    {
       var staves= await _repository.GetAll().ToListAsync();
        return View(staves);
    }
    
}