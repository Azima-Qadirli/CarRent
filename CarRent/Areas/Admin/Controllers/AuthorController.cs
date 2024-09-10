using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class AuthorController : Controller
{
    private readonly IRepository<Author> _repository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public AuthorController(IRepository<Author> repository,IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _repository = repository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var author = await _repository.GetAll().ToListAsync();
        return View(author);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Author author)
    {
        string fileName = Guid.NewGuid().ToString() + author.File.FileName;

        string path = _webHostEnvironment.WebRootPath + "/images/authors/"+fileName;
        using (FileStream stream = System.IO.File.Open(path, FileMode.Create))
        {
            await author.File.CopyToAsync(stream);
        };
        author.FileName = fileName;
        await _repository.AddAsync(author);
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
    public async Task<IActionResult> Update(int id, Author author)
    {
        var updatedAuthor = await _repository.GetAsync(id);
        updatedAuthor.Name = author.Name;
        updatedAuthor.Description = author.Description;
        if (author.File is not null)
        {
            string basePath = _webHostEnvironment.WebRootPath + "/images/authors";
            System.IO.File.Delete(basePath+updatedAuthor.FileName);
            string fileName = Guid.NewGuid() + author.File.FileName;

            string path = basePath +fileName;
            using (FileStream stream = System.IO.File.Open(path, FileMode.Create))
            {
                await author.File.CopyToAsync(stream);
            };
            updatedAuthor.FileName = fileName;
            
        }

        _repository.Update(updatedAuthor);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int id)
    {
        var author = await _repository.GetAsync(id);
        _repository.Remove(author);
        System.IO.File.Delete( _webHostEnvironment.WebRootPath + "/images/authors/"+ author.FileName);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }
}