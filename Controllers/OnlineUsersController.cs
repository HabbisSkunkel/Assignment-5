using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShop.Data;
using MusicShop.Models;

namespace MusicShop.Controllers
{
    public class OnlineUsersController : Controller
    {
        private readonly MusicShopContext _context;

        public OnlineUsersController(MusicShopContext context)
        {
            _context = context;
        }

        // GET: OnlineUsers
        public async Task<IActionResult> Index()
        {
            var musicShopContext = _context.OnlineUser.Include(o => o.ShoppingCart);
            return View(await musicShopContext.ToListAsync());
        }

        // GET: OnlineUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OnlineUser == null)
            {
                return NotFound();
            }

            var onlineUser = await _context.OnlineUser
                .Include(o => o.ShoppingCart)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (onlineUser == null)
            {
                return NotFound();
            }

            return View(onlineUser);
        }

        // GET: OnlineUsers/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.ShoppingCart, "RecordId", "RecordId");
            return View();
        }

        // POST: OnlineUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CartId,UserName")] OnlineUser onlineUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(onlineUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.ShoppingCart, "RecordId", "RecordId", onlineUser.CartId);
            return View(onlineUser);
        }

        // GET: OnlineUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OnlineUser == null)
            {
                return NotFound();
            }

            var onlineUser = await _context.OnlineUser.FindAsync(id);
            if (onlineUser == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.ShoppingCart, "RecordId", "RecordId", onlineUser.CartId);
            return View(onlineUser);
        }

        // POST: OnlineUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,CartId,UserName")] OnlineUser onlineUser)
        {
            if (id != onlineUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(onlineUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnlineUserExists(onlineUser.UserId))
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
            ViewData["CartId"] = new SelectList(_context.ShoppingCart, "RecordId", "RecordId", onlineUser.CartId);
            return View(onlineUser);
        }

        // GET: OnlineUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OnlineUser == null)
            {
                return NotFound();
            }

            var onlineUser = await _context.OnlineUser
                .Include(o => o.ShoppingCart)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (onlineUser == null)
            {
                return NotFound();
            }

            return View(onlineUser);
        }

        // POST: OnlineUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OnlineUser == null)
            {
                return Problem("Entity set 'MusicShopContext.OnlineUser'  is null.");
            }
            var onlineUser = await _context.OnlineUser.FindAsync(id);
            if (onlineUser != null)
            {
                _context.OnlineUser.Remove(onlineUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OnlineUserExists(int id)
        {
          return (_context.OnlineUser?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
