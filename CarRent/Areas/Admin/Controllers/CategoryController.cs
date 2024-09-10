//using CarRent.Models;
using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class CategoryController : Controller
{
    private readonly IRepository<Category> _repository;

    public CategoryController(IRepository<Category> repository)
    {
        _repository = repository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var model = await _repository.GetAll().ToListAsync();
        return View(model);
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Category Category)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _repository.AddAsync(Category);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var Category = await _repository.GetAsync(id);
        return View(Category);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Category Category)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var updatedCategory = await _repository.GetAsync(id);
        updatedCategory.Name = Category.Name;
        
        _repository.Update(updatedCategory);
        await  _repository.SaveAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int id)
    {
        await _repository.RemoveAsync(id);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }
}