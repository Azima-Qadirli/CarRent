using System.Configuration;
using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class ContactController : Controller
{
    private readonly IRepository<Message>_repository;
    private readonly UserManager<AppUser> _userManager;
    public ContactController(IRepository<Message> repository, UserManager<AppUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }
[HttpPost]
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> SendEmail([FromBody] Message message)
    {
        if (!ModelState.IsValid)
        {
            return Json(new{successCode = 400,message = "Please,check your inputs!"});
        }
      var user = await  _userManager.FindByNameAsync(User.Identity?.Name);
      if (user == null)
      {
          return Json(new{successCode = 404,message = "First,You have to login."});
      }
      message.AppUserId = user.Id;
        await _repository.AddAsync(message);
        await _repository.SaveAsync();
        return Json(new{successCode = 201,message = "Your message has been sent."});
    }
}