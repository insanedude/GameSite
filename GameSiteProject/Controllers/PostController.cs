using System;
using System.Collections.Generic;
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
    public class PostController : BaseController
    {
        private readonly GameSiteDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<HomeController> _localizer;

        public PostController(GameSiteDbContext context, UserManager<User> userManager,
            IStringLocalizer<HomeController> localizer) : base(localizer, userManager)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }
        
        // GET: Post
        public async Task<IActionResult> Index()
        {
            var gameSiteDbContext = _context.Posts.Include(p => p.ForumThread).Include(p => p.User);
            SetNicknameAsync().Wait();
            return View(await gameSiteDbContext.ToListAsync());
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.ForumThread)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            ViewData["ForumThreadId"] = new SelectList(_context.ForumThreads, "ForumThreadId", "ForumThreadId");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Content,DatePosted,IsEdited,ForumThreadId,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ForumThreadId"] = new SelectList(_context.ForumThreads, "ForumThreadId", "ForumThreadId", post.ForumThreadId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["ForumThreadId"] = new SelectList(_context.ForumThreads, "ForumThreadId", "ForumThreadId", post.ForumThreadId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Content,DatePosted,IsEdited,ForumThreadId,UserId")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            ViewData["ForumThreadId"] = new SelectList(_context.ForumThreads, "ForumThreadId", "ForumThreadId", post.ForumThreadId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.ForumThread)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
