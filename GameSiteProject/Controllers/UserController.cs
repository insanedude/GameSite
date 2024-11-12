using System.Linq;
using System.Threading.Tasks;
using GameSiteProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameSiteProject.Controllers;

public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    // private readonly IdentityDbContext<User> _context;
    public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        // , IdentityDbContext<User> context
        _userManager = userManager;
        _signInManager = signInManager;
        // _context = context;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        // Passing the nickname to the view (layout file in this case)
        ViewData["Nickname"] = user.Nickname;

        UserViewModel uvm = new UserViewModel()
        {
            Nickname = user.Nickname,
            Email = user.Email,
            ProfilePicturePath = user.ProfilePicturePath,
            UserInformation = user.UserInformation,
            TotalScore = user.TotalScore,
            DateJoined = user.DateJoined
        };
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
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
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
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    // GET: User/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }
    
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.Users
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Edit(string id, EditViewModel evm)
    // {
    //     var user = await _userManager.FindByIdAsync(evm.Id);
    //     // if (user == null)
    //     // {
    //     //     return NotFound();
    //     // }
    //     if (id != user.Id)
    //     {
    //         return NotFound();
    //     }
    //
    //     if (ModelState.IsValid)
    //     {
    //         user.Nickname = evm.Nickname;
    //         await _userManager.UpdateAsync(user);
    //     }
    //     return View(user);
    // }
    
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.Users
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        ViewData["HideHeader"] = true; // Set flag to hide header in this view
        return View(user);
    }
    
    [HttpGet]
    [Route("User/EditUser")]
    public async Task<IActionResult> EditUser()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var model = new EditViewModel
        {
            Id = user.Id,
            Nickname = user.Nickname,
            Email = user.Email,
            ProfilePicturePath = user.ProfilePicturePath,
            UserInformation = user.UserInformation
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(EditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        user.Nickname = model.Nickname;
        user.Email = model.Email;
        user.ProfilePicturePath = model.ProfilePicturePath;
        user.UserInformation = model.UserInformation;

        if (!string.IsNullOrEmpty(model.Password))
        {
            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (removePasswordResult.Succeeded)
            {
                await _userManager.AddPasswordAsync(user, model.Password);
            }
        }

        await _userManager.UpdateAsync(user);
        return RedirectToAction("Profile");
    }
}