using CarRent.Repositories.Interfaces;
using CarRent.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class BlogController : Controller
{
    private readonly IRepository<Blog> _repository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Tag> _tagRepository;
    private readonly IRepository<BlogTag> _blogTagRepository;
    private readonly IRepository<Author> _authorRepository;
    public BlogController(IRepository<Blog> repository,IWebHostEnvironment webHostEnvironment, IRepository<Category> categoryRepository, IRepository<Tag> tagRepository, IRepository<BlogTag> blogTagRepository, IRepository<Author> authorRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
        _blogTagRepository = blogTagRepository;
        _authorRepository = authorRepository;
        _repository = repository;
    }

    // GET
    public async  Task<IActionResult> Index()
    {
       var models= await _repository.GetAll().Include(x=>x.Category).Include(x=>x.Author).ToListAsync();
        return View(models);
    }

    [HttpGet]
    public async  Task<IActionResult> Add()
    {
        ViewBag.Categories = await _categoryRepository.GetAll().ToListAsync();
        ViewBag.Tags = await _tagRepository.GetAll().ToListAsync();
        ViewBag.Authors = await _authorRepository.GetAll().ToListAsync();
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
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        ViewBag.Categories = await _categoryRepository.GetAll().ToListAsync();
        ViewBag.Tags = await _tagRepository.GetAll().Include(x=>x.BlogTags).ToListAsync();
        ViewBag.Authors = await _authorRepository.GetAll().ToListAsync();
        var data = await _repository.GetAsync(id);
        return View(data);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(int id, Blog Blog)
    {
        var updatedBlog = await _repository.GetAll().Include(x=>x.BlogTags).FirstOrDefaultAsync(x => x.Id == id);
        updatedBlog.Title1 = Blog.Title1;
        updatedBlog.Description1 = Blog.Description1;
        updatedBlog.Title2 = Blog.Title2;
        updatedBlog.Description2 = Blog.Description2;
        updatedBlog.Paragraph = Blog.Paragraph;
        updatedBlog.AuthorId = Blog.AuthorId;
        updatedBlog.CategoryId = Blog.CategoryId;
    
        if (Blog.File is not null)
        {
            string basePath = _webHostEnvironment.WebRootPath + "/images/staves/";
            System.IO.File.Delete(basePath+updatedBlog.FileName);
            string fileName = Guid.NewGuid() + Blog.File.FileName;
    
            string path = basePath +fileName;
            using (FileStream stream = System.IO.File.Open(path, FileMode.Create))
            {
                await Blog.File.CopyToAsync(stream);
            };
            updatedBlog.FileName = fileName;
            
        }

        List<BlogTag> removedTags = new List<BlogTag>();
        foreach (var blogTag in updatedBlog.BlogTags)//2,3
        {
            bool result = false;
            foreach (var tagID in Blog.TagIDs)//1,2
                if(blogTag.TagId == tagID)
                    result = true;
            if(!result)
                updatedBlog.BlogTags.Remove(blogTag);
        }

        foreach (var blogTagId in Blog.TagIDs)
        {
            if (!updatedBlog.BlogTags.Any(x => x.TagId == blogTagId))
            {
                updatedBlog.BlogTags.Add(new BlogTag()
                {
                    TagId = blogTagId,
                    Blog = Blog
                });
            }
        }
        
        
        _repository.Update(updatedBlog);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Remove(int id)
    {
        var blog = await _repository.GetAsync(id);
        _repository.Remove(blog);
        System.IO.File.Delete( _webHostEnvironment.WebRootPath + "/images/blogs/"+ blog.FileName);
        await _repository.SaveAsync();
        return RedirectToAction("Index");
    }
}