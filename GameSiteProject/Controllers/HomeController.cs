using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using GameSiteProject.Models;
using GameSiteProject.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GameSiteProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly GameSiteDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, GameSiteDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        private async Task SetNicknameAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    ViewBag.Nickname = user.Nickname;
                }
            }
        }
        public async Task<IActionResult> Index()
        {
            await SetNicknameAsync();

            var forumThreads = await _context.ForumThreads
                .Include(f => f.Game)
                .Include(f => f.User)
                .OrderByDescending(f => f.DateCreated)
                .ToListAsync();

            return View(forumThreads);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}