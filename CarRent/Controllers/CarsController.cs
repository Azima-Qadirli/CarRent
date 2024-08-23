using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class CarsController : Controller
{
   
    public IActionResult Index()
    {
        return View();
    }
}