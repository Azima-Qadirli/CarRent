using System.Configuration;
using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class ContactController : Controller
{
    //private readonly IRepository<Message>_repository;
    //private readonly UserManager<AppUser> _userManager;
    //public ContactController(IRepository<Message> repository, UserManager<AppUser> userManager)
    //{
    //    _repository = repository;
    //    _userManager = userManager;
    //}
    //public IActionResult Index()
    //{
    //    return View();
    //}
    //[HttpPost]
    //public async Task<IActionResult> SendMessage([FromBody] Message message)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return Json(new { successCode = 400, message = "Please check your inputs" });
    //    }

    //    try
    //    {
    //        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
    //        message.AppUserId = user.Id;
    //    }
    //    catch 
    //    {
    //        return Json(new { successCode = 404, message = "Firstly, you have to login" });
    //    }

    //    await _repository.AddAsync(message);
    //    await _repository.SaveAsync();
    //    return Json(new { successCode = 201, message = "Your message has been sent" });
    //}
}