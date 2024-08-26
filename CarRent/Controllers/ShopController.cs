using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class ShopController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}