using Microsoft.AspNetCore.Mvc;

namespace CarRent.Areas.Admin.Controllers;
[Area("Admin")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}