using System.Diagnostics;
using CarRent.Repositories.Interfaces;
using CarRent.ViewModels;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarRent.Controllers;

public class HomeController : Controller
{

    private readonly IRepository<Staff> _repository;
    private readonly IRepository<Carousel> _carouselRepository;

    public HomeController(IRepository<Staff> repository, IRepository<Carousel> carouselRepository)
    {
        _repository = repository;
        _carouselRepository = carouselRepository;
    }

    public async  Task<ActionResult> Index()
    {

        HomeVM model = new()
        {
            Staves = await _repository.GetAll().Take(4).ToListAsync(),
            Carousels = await _carouselRepository.GetAll().ToListAsync()
        };
        return View(model);
    }
    
}