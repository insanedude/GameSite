using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameSiteProject.Models;
using GameSiteProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace GameSiteProject.Controllers
{
    public class MessageController : BaseController
    {
        private readonly GameSiteDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<HomeController> _localizer;


        public MessageController(GameSiteDbContext context, UserManager<User> userManager, 
            IStringLocalizer<HomeController> localizer) : base(localizer, userManager)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }
        
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            await SetNicknameAsync();
    
            if (currentUser == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Where(m => (m.SenderId == currentUser.Id || m.ReceiverId == currentUser.Id) && m.Receiver != null)
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.DateSent)
                .ToListAsync();

            return View(messages);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            await SetNicknameAsync();
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        public IActionResult Create()
        {
            ViewData["ReceiverId"] = new SelectList(_context.User, "Id", "Id");
            ViewData["SenderId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,SenderId,ReceiverId,Content,DateSent,IsRead")] Message message)
        {
            await SetNicknameAsync();
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReceiverId"] = new SelectList(_context.User, "Id", "Id", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.User, "Id", "Id", message.SenderId);
            return View(message);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            await SetNicknameAsync();
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["ReceiverId"] = new SelectList(_context.User, "Id", "Id", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.User, "Id", "Id", message.SenderId);
            return View(message);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,SenderId,ReceiverId,Content,DateSent,IsRead")] Message message)
        {
            await SetNicknameAsync();

            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
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
            ViewData["ReceiverId"] = new SelectList(_context.User, "Id", "Id", message.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.User, "Id", "Id", message.SenderId);
            return View(message);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            await SetNicknameAsync();

            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await SetNicknameAsync();

            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
        
        public IActionResult SendPrivateMessage()
        {
            SetNicknameAsync().Wait();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendPrivateMessage(PrivateMessageViewModel model)
        {
            await SetNicknameAsync();

            if (ModelState.IsValid)
            {
                var sender = await _userManager.GetUserAsync(User);
                if (sender == null)
                {
                    return NotFound();
                }
                
                var receiver = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.Nickname == model.ReceiverNickname); 

                if (receiver == null)
                {
                    ModelState.AddModelError("ReceiverNickname", "Receiver not found.");
                    return View(model);
                }

                var message = new Message
                {
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id,
                    Content = model.Content,
                    DateSent = DateTime.Now,
                    IsRead = false
                };

                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Inbox));
            }
            return View(model);
        }
        
        public async Task<IActionResult> Inbox()
        {
            await SetNicknameAsync();

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Where(m => (m.SenderId == currentUser.Id || m.ReceiverId == currentUser.Id) && m.Receiver != null)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .OrderByDescending(m => m.DateSent)
                .ToListAsync();

            return View(messages);
        }
    }
}
