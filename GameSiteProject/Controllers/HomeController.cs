using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using GameSiteProject.Models;
using GameSiteProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GameSiteProject.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly GameSiteDbContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, 
            GameSiteDbContext context, IStringLocalizer<HomeController> localizer) : base(localizer, userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index(string nickname = null, string sortOrder = "desc")
        {
            await SetNicknameAsync();

            var query = _context.ForumThreads
                .Include(f => f.Game)
                .Include(f => f.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nickname))
            {
                query = query.Where(f => f.User.Nickname == nickname);
            }

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
        
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}