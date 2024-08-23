using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace CarRent.Controllers;

public class HomeController : Controller
{
    
   
    public IActionResult Index()
    {
        return View();
    }

    
}