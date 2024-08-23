using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class _404Controller : Controller
{
   
    public IActionResult Index()
    {
        return View();
    }
}