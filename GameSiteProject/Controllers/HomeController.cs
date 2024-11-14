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

        public async Task<IActionResult> Index(string nickname = null, string sortOrder = "desc")
        {
            await SetNicknameAsync();

            var query = _context.ForumThreads
                .Include(f => f.Game)
                .Include(f => f.User)
                .AsQueryable();

            // Filter by nickname if provided
            if (!string.IsNullOrEmpty(nickname))
            {
                query = query.Where(f => f.User.Nickname == nickname);
            }

            // Sorting logic
            if (sortOrder == "asc")
            {
                query = query.OrderBy(f => f.DateCreated);
            }
            else
            {
                query = query.OrderByDescending(f => f.DateCreated);
            }

            var forumThreads = await query.ToListAsync();

            ViewBag.CurrentNicknameFilter = nickname;
            ViewBag.CurrentSortOrder = sortOrder;
            return View(forumThreads);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}