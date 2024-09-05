using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers;
[Area("Admin")]
public class BlogController : Controller
{
    private readonly IRepository<Blog> _repository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Tag> _tagRepository;
    private readonly IRepository<BlogTag> _blogTagRepository;
    public BlogController(IRepository<Blog> repository,IWebHostEnvironment webHostEnvironment, IRepository<Category> categoryRepository, IRepository<Tag> tagRepository, IRepository<BlogTag> blogTagRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
        _blogTagRepository = blogTagRepository;
        _repository = repository;
    }

    // GET
    public async  Task<IActionResult> Index()
    {
       var models= await _repository.GetAll().Include(x=>x.Category).ToListAsync();
        return View(models);
    }

    [HttpGet]
    public async  Task<IActionResult> Add()
    {
        ViewBag.Categories = await _categoryRepository.GetAll().ToListAsync();
        ViewBag.Tags = await _tagRepository.GetAll().ToListAsync(); 
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Blog Blog)
    {
        string fileName = Guid.NewGuid().ToString() + Blog.File.FileName;

        string path = _webHostEnvironment.WebRootPath + "/images/blogs/"+fileName;
        using (FileStream stream = System.IO.File.Open(path, FileMode.Create))
        {
            await Blog.File.CopyToAsync(stream);
        };
        Blog.FileName = fileName;
        foreach (var tagId in Blog.TagIDs)
        {
            await _blogTagRepository.AddAsync(new BlogTag()
            {
                TagId = tagId,
                Blog = Blog
            });
        }
        
        await _repository.AddAsync(Blog);
        await _repository.SaveAsync();
        return RedirectToAction("Index");

    }
    
    // [HttpGet]
    // public async Task<IActionResult> Update(int id)
    // {
    //     var data = await _repository.GetAsync(id);
    //     return View(data);
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> Update(int id, Blog Blog)
    // {
    //     var updatedBlog = await _repository.GetAsync(id);
    //     updatedBlog.FirstName = Blog.FirstName;
    //     updatedBlog.LastName = Blog.LastName;
    //     updatedBlog.Profession = Blog.Profession;
    //
    //     if (Blog.File is not null)
    //     {
    //         string basePath = _webHostEnvironment.WebRootPath + "/images/staves/";
    //         System.IO.File.Delete(basePath+updatedBlog.FileName);
    //         string fileName = Guid.NewGuid() + Blog.File.FileName;
    //
    //         string path = basePath +fileName;
    //         using (FileStream stream = System.IO.File.Open(path, FileMode.Create))
    //         {
    //             await Blog.File.CopyToAsync(stream);
    //         };
    //         updatedBlog.FileName = fileName;
    //         
    //     }
    //     _repository.Update(updatedBlog);
    //     await _repository.SaveAsync();
    //     return RedirectToAction("Index");
    // }
    //
    // public async Task<IActionResult> Remove(int id)
    // {
    //     var Blog = await _repository.GetAsync(id);
    //     _repository.Remove(Blog);
    //     System.IO.File.Delete( _webHostEnvironment.WebRootPath + "/images/staves/"+ Blog.FileName);
    //     await _repository.SaveAsync();
    //     return RedirectToAction("Index");
    // }
}