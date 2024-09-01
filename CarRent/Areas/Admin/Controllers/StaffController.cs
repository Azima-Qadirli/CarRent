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
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Staff staff)
    {
        var updatedStaff = await _repository.GetAsync(id);
        updatedStaff.FirstName = staff.FirstName;
        updatedStaff.LastName = staff.LastName;
        updatedStaff.Profession = staff.Profession;

        if (staff.File is not null)
        {
            string basePath = _webHostEnvironment.WebRootPath + "/images/staves/";
            System.IO.File.Delete(basePath+updatedStaff.FileName);
            string fileName = Guid.NewGuid() + staff.File.FileName;

            string path = basePath +fileName;
            using (FileStream stream = System.IO.File.Open(path, FileMode.Create))
            {
                await staff.File.CopyToAsync(stream);
            };
            updatedStaff.FileName = fileName;
            
        }
        _repository.Update(updatedStaff);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int id)
    {
        var staff = await _repository.GetAsync(id);
        _repository.Remove(staff);
        System.IO.File.Delete( _webHostEnvironment.WebRootPath + "/images/staves/"+ staff.FileName);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }
}