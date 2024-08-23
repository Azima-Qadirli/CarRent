using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class TeamController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
}