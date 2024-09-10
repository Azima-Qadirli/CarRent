using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class TagController : Controller
{
    private readonly IRepository<Tag> _repository;

    public TagController(IRepository<Tag> repository)
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
    public async Task<IActionResult> Add(Tag Tag)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _repository.AddAsync(Tag);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var Tag = await _repository.GetAsync(id);
        return View(Tag);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id,Tag Tag)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var updateTag = await _repository.GetAsync(id);
        updateTag.Name = Tag.Name;
        _repository.Update(updateTag);
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