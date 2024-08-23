using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class AboutController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
}