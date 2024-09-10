using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class CarouselController : Controller
{
    private readonly IRepository<Carousel> _repository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public CarouselController(IRepository<Carousel> repository,IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _repository = repository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var carousel = await _repository.GetAll().ToListAsync();
        return View(carousel);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Carousel carousel)
    {
        string fileName = Guid.NewGuid().ToString() + carousel.File.FileName;

        string path = _webHostEnvironment.WebRootPath + "/images/carousels/"+fileName;
        using (FileStream stream = System.IO.File.Open(path, FileMode.Create))
        {
            await carousel.File.CopyToAsync(stream);
        };
        carousel.FileName = fileName;
        await _repository.AddAsync(carousel);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var data = await _repository.GetAsync(id);
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Carousel carousel)
    {
        var updatedCarousel = await _repository.GetAsync(id);
        updatedCarousel.Title = carousel.Title;
        updatedCarousel.Description = carousel.Description;
        if (carousel.File is not null)
        {
            string basePath = _webHostEnvironment.WebRootPath + "/images/carousels";
            System.IO.File.Delete(basePath+updatedCarousel.FileName);
            string fileName = Guid.NewGuid() + carousel.File.FileName;

            string path = basePath +fileName;
            using (FileStream stream = System.IO.File.Open(path, FileMode.Create))
            {
                await carousel.File.CopyToAsync(stream);
            };
            updatedCarousel.FileName = fileName;
            
        }

        _repository.Update(updatedCarousel);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int id)
    {
        var carousel = await _repository.GetAsync(id);
        _repository.Remove(carousel);
        System.IO.File.Delete( _webHostEnvironment.WebRootPath + "/images/carousels/"+ carousel.FileName);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }
}