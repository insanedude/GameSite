using System.Threading.Tasks;
using GameSiteProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameSiteProject.Controllers;

public class UserController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel model, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            User user = new() { Email = model.Email, UserName = model.Email, Nickname = model.Nickname};
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Search", "Home");
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Search", "Home");
                }
            }
        }
        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Search", "Home");
    }
}