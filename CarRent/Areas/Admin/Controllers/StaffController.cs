using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers;
[Area("Admin")]
public class StaffController : Controller
{
    private readonly IRepository<Staff> _repository;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public StaffController(IRepository<Staff> repository,IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _repository = repository;
    }

    // GET
    public async  Task<IActionResult> Index()
    {
       var models= await _repository.GetAll().ToListAsync();
        return View(models);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Staff staff)
    {
        string fileName = Guid.NewGuid().ToString() + staff.File.FileName;

        string path = _webHostEnvironment.WebRootPath + "/images/staves/"+fileName;
        using (FileStream stream = System.IO.File.Open(path, FileMode.Create))
        {
            await staff.File.CopyToAsync(stream);
        };
        staff.FileName = fileName;
        
        await _repository.AddAsync(staff);
        await _repository.SaveAsync();
        return RedirectToAction("Index");

    }
    
    

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var data = await _repository.GetAsync(id);
        return View();
    }
}