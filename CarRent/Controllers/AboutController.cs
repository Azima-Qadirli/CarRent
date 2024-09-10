
using CarRent.Repositories.Interfaces;
using CarRent.ViewModels;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Controllers;

public class AboutController : Controller
{
    private IRepository<Staff>_staffRepository;

    public AboutController(IRepository<Staff> staffRepository)
    {
        _staffRepository = staffRepository;
    }
[HttpGet]
    public async  Task<IActionResult> Index()
    {
        AboutVM model = new AboutVM()
        {
            Staves = await _staffRepository.GetAll().Take(4).ToListAsync()
        };
        return View(model);
    }
}