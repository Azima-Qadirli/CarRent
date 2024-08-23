using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class ServiceController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
}