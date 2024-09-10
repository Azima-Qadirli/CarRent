using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers;

[Area("Admin")]

public class MessageController : Controller
{
    private readonly IRepository<Message> _repository;

    public MessageController(IRepository<Message> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Index()
    {
        var models = await _repository.GetAll().ToListAsync();
        return View(models);
    }


    public async Task<IActionResult> Details(int id)
    {
        var result = await _repository.GetAll().Include(x=>x.AppUser).Where(x => x.Id == id).ToListAsync();
        if (result is null)
        {
            return Json(new{message = "Not Found"});
        }
        return Json(result);
    }
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Remove(int id)
    {
        await _repository.RemoveAsync(id);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }
}