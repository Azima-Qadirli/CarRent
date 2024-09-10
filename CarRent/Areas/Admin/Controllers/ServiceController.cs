using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class ServiceController : Controller
{
    private readonly IRepository<Service> _repository;

    public ServiceController(IRepository<Service> repository)
    {
        _repository = repository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var models = await _repository.GetAll().ToListAsync();
        return View(models);
    }
[HttpGet]
    public IActionResult Add()
    {
        return View();
    }
[HttpPost]
    public async Task<IActionResult> Add(Service service)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _repository.AddAsync(service);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var service = await _repository.GetAsync(id);
        return View(service);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id,Service service)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var updateService = await _repository.GetAsync(id);
        updateService.Title = service.Title;
        updateService.Description = service.Description;
        updateService.Icon = service.Icon;
        _repository.Update(updateService);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int id)
    {
        await _repository.RemoveAsync(id);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }
}