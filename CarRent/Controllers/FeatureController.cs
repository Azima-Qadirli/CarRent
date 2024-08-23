using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class FeatureController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
}