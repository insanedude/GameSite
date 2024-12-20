using System.Linq;
using System.Threading.Tasks;
using GameSiteProject.Models;
using GameSiteProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GameSiteProject.Controllers;

public class UserController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly GameSiteDbContext _context;
    private readonly IStringLocalizer<HomeController> _localizer;

    public UserController(GameSiteDbContext context, UserManager<User> userManager,
        IStringLocalizer<HomeController> localizer, SignInManager<User> signInManager) : base(localizer, userManager)
    {
        _context = context;
        _userManager = userManager;
        _localizer = localizer;
        _signInManager = signInManager;
    }
    
    public async Task<IActionResult> Index()
    {
        SetNicknameAsync().Wait();
        var users = await _userManager.Users.Select(u => new UserViewModel
        {
            Id = u.Id,
            Nickname = u.Nickname,
            Email = u.Email,
            ProfilePicturePath = u.ProfilePicturePath,
            UserInformation = u.UserInformation,
            TotalScore = u.TotalScore,
            DateJoined = u.DateJoined
        }).ToListAsync();

        return View(users);
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        SetNicknameAsync().Wait();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel model, string? returnUrl = null)
    {
        await SetNicknameAsync();
        if (ModelState.IsValid)
        {
            User user = new() { Email = model.Email, UserName = model.Email, Nickname = model.Nickname, 
                ProfilePicturePath = model.ProfilePicturePath, UserInformation = model.UserInformation};
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
        SetNicknameAsync().Wait();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        await SetNicknameAsync();
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

    [HttpGet]
    public async Task<IActionResult> Edit(string? id)
    {
        await SetNicknameAsync();
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
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
    public async Task<IActionResult> Edit(string id, EditViewModel evm)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null || id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            user.Nickname = evm.Nickname;
            user.Email = evm.Email;
            user.ProfilePicturePath = evm.ProfilePicturePath;
            user.UserInformation = evm.UserInformation;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Profile");
            }
            AddErrors(result);
        }

        return View(evm);
    }
    
    public async Task<IActionResult> Details(string? id)
    {
        await SetNicknameAsync();

        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.Users
            .Include(u => u.ForumThreads)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        var model = new UserViewModel
        {
            Nickname = user.Nickname,
            Email = user.Email,
            ProfilePicturePath = user.ProfilePicturePath,
            UserInformation = user.UserInformation,
            TotalScore = user.TotalScore,
            DateJoined = user.DateJoined
        };

        return View(model);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Delete(string? id)
    {
        await SetNicknameAsync();

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
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        AddErrors(result);
        return View(user);
    }
    
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        await SetNicknameAsync();

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        ViewData["HideHeader"] = true;
        return View(user);
    }
    
    [HttpGet]
    [Route("User/EditUser")]
    public async Task<IActionResult> EditUser()
    {
        await SetNicknameAsync();

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
        await SetNicknameAsync();

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
    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}