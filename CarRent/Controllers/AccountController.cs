using CarRent.Services;
using CarRent.Views.Models;
using CarRent.Views.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMailService _mailService;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailService mailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mailService = mailService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        AppUser user = new AppUser()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
        };
        var identityResult= await _userManager.CreateAsync(user, model.Password);
        if (!identityResult.Succeeded)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        await _userManager.AddToRoleAsync(user, "User");
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var link = Url.Action("ConfirmEmail", "Account", new
            {
                userId = user.Id, token = token
            },protocol: HttpContext.Request.Scheme
        );

        _mailService.SendMail(user.Email, "Verify Email",$"Please click this link {link} to verify your email"); 
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
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

       if (!await _userManager.IsInRoleAsync(user, "User"))
       {
           ModelState.AddModelError("", "You do not have permission to access this page.");
           return View(model);
           
       }
       
       var result= await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
       if (!result.Succeeded)
       {
           ModelState.AddModelError("", "Invalid username or password.");
           return View(model);
       }
       return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult>ConfirmEmail(string userId, string token)
    {
        var user  = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return NotFound();
        }
        await _userManager.ConfirmEmailAsync(user, token);
        await _signInManager.SignInAsync(user,false);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult ForgetPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
    {
     var user = await _userManager.FindByEmailAsync(model.Email);
     if (user is null)
     {
         return NotFound();
     }
     var token = await _userManager.GeneratePasswordResetTokenAsync(user);
     var link = Url.Action("ResetPassword", "Account", new{userId = user.Id, token}, protocol: HttpContext.Request.Scheme);
     _mailService.SendMail(user.Email, "Reset Password",$"Please click this link {link} to reset your password"); 
     return RedirectToAction("Index", "Home");
    }
[HttpGet]
    public IActionResult ResetPassword(string userId, string token)
    {
        ResetPasswordModel model = new ResetPasswordModel()
        {
            UserId = userId,
            Token = token
        };
        return View(token);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user is null)
        {
            return NotFound();
        }
        IdentityResult result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
                
            }
            return View(model);
        }
        return RedirectToAction("login", "account");
    }
    
}