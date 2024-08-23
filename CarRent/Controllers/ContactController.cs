using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class ContactController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
}