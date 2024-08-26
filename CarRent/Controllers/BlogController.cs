using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class BlogController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Detail()
    {
        return View();
    }
}