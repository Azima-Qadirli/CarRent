using CarRent.Views.Models;
using CarRent.Views.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Areas.Admin.Controllers;
[Area("Admin")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UsernameOrEmail);

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);

            if (user is null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }
        }

        if (!await _userManager.IsInRoleAsync(user, "Admin") && !await _userManager.IsInRoleAsync(user, "SuperAdmin"))
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }

        if (!await _userManager.IsInRoleAsync(user, "Admin") && !await _userManager.IsInRoleAsync(user, "SuperAdmin"))
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }
        
        
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }
        
        return RedirectToAction("index","home");
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("index", "home","default");
    }
    
    // public async Task<IActionResult> AddAdmin()
    // {
    //     var user = new AppUser()
    //     {
    //         Email = "superadmin@cental.az",
    //         FirstName = "SuperAdmin",
    //         LastName = "SuperAdmin",
    //         UserName = "SuperAdmin",
    //         EmailConfirmed = true,
    //     };
    //    await  _userManager.CreateAsync(user, "SuperAdmin123.");
    //    await _userManager.AddToRolesAsync(user,new List<string>{"Admin","SuperAdmin"});
    //    return Json("Yarandiiii");
    // }
    
    
    
    // public async Task<IActionResult> AddRole()
    // {
    //     var role1 = new IdentityRole()
    //     {
    //         Name = "Admin"
    //     };
    //     var role2 = new IdentityRole()
    //     {
    //         Name = "SuperAdmin"
    //     };
    //     var role3 = new IdentityRole()
    //     {
    //         Name = "User"
    //     };
    //     await _roleManager.CreateAsync(role1);
    //     await _roleManager.CreateAsync(role2);
    //     await _roleManager.CreateAsync(role3);
    //     return Json("Yarandiiii");
    //     
    // }
}