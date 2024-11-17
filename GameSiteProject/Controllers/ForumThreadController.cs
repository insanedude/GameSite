using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameSiteProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace GameSiteProject.Controllers
{
    public class ForumThreadController : BaseController
    {
        private readonly GameSiteDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<HomeController> _localizer;

        public ForumThreadController(GameSiteDbContext context, UserManager<User> userManager,
            IStringLocalizer<HomeController> localizer) : base(localizer, userManager)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }
        
        public async Task<IActionResult> Index()
        {
            await SetNicknameAsync();
            var gameSiteDbContext = _context.ForumThreads.Include(f => f.Game).Include(f => f.User);
            return View(await gameSiteDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            await SetNicknameAsync();

            if (id == null)
            {
                return NotFound();
            }

            var forumThread = await _context.ForumThreads
                .Include(f => f.Game)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.ForumThreadId == id);
            if (forumThread == null)
            {
                return NotFound();
            }

            return View(forumThread);
        }

        public IActionResult Create()
        {
            SetNicknameAsync().Wait();
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameId");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForumThreadId,Title,GameId,Description,UserId,DateCreated,LastUpdated,ViewsCount")] ForumThread forumThread)
        {
            await SetNicknameAsync();

            if (ModelState.IsValid)
            {
                _context.Add(forumThread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameId", forumThread.GameId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", forumThread.UserId);
            return View(forumThread);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            await SetNicknameAsync();

            if (id == null)
            {
                return NotFound();
            }

            var forumThread = await _context.ForumThreads.FindAsync(id);
            if (forumThread == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameId", forumThread.GameId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", forumThread.UserId);
            return View(forumThread);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForumThreadId,Title,GameId,Description,UserId,DateCreated,LastUpdated,ViewsCount")] ForumThread forumThread)
        {
            await SetNicknameAsync();

            if (id != forumThread.ForumThreadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumThread);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumThreadExists(forumThread.ForumThreadId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameId", forumThread.GameId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", forumThread.UserId);
            return View(forumThread);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            await SetNicknameAsync();

            if (id == null)
            {
                return NotFound();
            }

            var forumThread = await _context.ForumThreads
                .Include(f => f.Game)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.ForumThreadId == id);
            if (forumThread == null)
            {
                return NotFound();
            }

            return View(forumThread);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await SetNicknameAsync();

            var forumThread = await _context.ForumThreads.FindAsync(id);
            if (forumThread != null)
            {
                _context.ForumThreads.Remove(forumThread);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumThreadExists(int id)
        {
            return _context.ForumThreads.Any(e => e.ForumThreadId == id);
        }
        
        public IActionResult CreateDiscussion()
        {
            SetNicknameAsync().Wait();
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Title");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDiscussion([Bind("ForumThreadId,Title,GameId,Description")] ForumThread forumThread)
        {
            await SetNicknameAsync();
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    forumThread.UserId = user.Id;
                }

                forumThread.DateCreated = DateTime.Now;
                forumThread.LastUpdated = DateTime.Now;
                forumThread.ViewsCount = 0;

                _context.Add(forumThread);
                await _context.SaveChangesAsync();

                return Redirect("https://localhost:5001/");
            }

            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Title", forumThread.GameId);
            return View(forumThread);
        }
        
        public async Task<IActionResult> ViewDiscussion(int? id)
        {
            await SetNicknameAsync();
            if (id == null)
            {
                return NotFound();
            }

            var forumThread = await _context.ForumThreads
                .Include(f => f.Game)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.ForumThreadId == id);

            if (forumThread == null)
            {
                return NotFound();
            }

            var comments = await _context.Messages
                .Where(m => m.ForumThreadId == forumThread.ForumThreadId)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.DateSent)
                .ToListAsync();

            ViewBag.Comments = comments;

            return View("ViewDiscussion", forumThread);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int forumThreadId, string content)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }

            var message = new Message
            {
                SenderId = user.Id,
                ReceiverId = null,
                Content = content,
                DateSent = DateTime.Now,
                IsRead = false,
                ForumThreadId = forumThreadId
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var comments = await _context.Messages
                .Where(m => m.ForumThreadId == forumThreadId)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.DateSent)
                .ToListAsync();

            var forumThread = await _context.ForumThreads
                .Include(f => f.Game)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.ForumThreadId == forumThreadId);

            ViewBag.Comments = comments;

            return RedirectToAction("ViewDiscussion", new { id = forumThreadId });        
        }
    }
}
