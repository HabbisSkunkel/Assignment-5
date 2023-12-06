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
    public class ShoppingCartsController : Controller
    {
        private readonly MusicShopContext _context;

        public ShoppingCartsController(MusicShopContext context)
        {
            _context = context;
        }

        // GET: ShoppingCarts
        [AdminAuthorizationFilter]
        public async Task<IActionResult> Index()
        {
            var musicShopContext = _context.ShoppingCart.Include(s => s.OnlineUser).Include(s => s.Song);
            return View(await musicShopContext.ToListAsync());
        }

        // GET: ShoppingCarts/Details/5
        [AdminAuthorizationFilter]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(s => s.OnlineUser)
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        [AdminAuthorizationFilter]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.OnlineUser, "UserId", "UserName");
            ViewData["SongId"] = new SelectList(_context.Song, "SongId", "Title");
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorizationFilter]
        public async Task<IActionResult> Create([Bind("RecordId,UserId,SongId,Count")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.OnlineUser, "UserId", "UserName", shoppingCart.UserId);
            ViewData["SongId"] = new SelectList(_context.Song, "SongId", "Title", shoppingCart.SongId);
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Edit/5
        [AdminAuthorizationFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.OnlineUser, "UserId", "UserName", shoppingCart.UserId);
            ViewData["SongId"] = new SelectList(_context.Song, "SongId", "Title", shoppingCart.SongId);
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorizationFilter]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,UserId,SongId,Count")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.RecordId))
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
            ViewData["UserId"] = new SelectList(_context.OnlineUser, "UserId", "UserName", shoppingCart.UserId);
            ViewData["SongId"] = new SelectList(_context.Song, "SongId", "Title", shoppingCart.SongId);
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Delete/5
        [AdminAuthorizationFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShoppingCart == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(s => s.OnlineUser)
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminAuthorizationFilter]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShoppingCart == null)
            {
                return Problem("Entity set 'MusicShopContext.ShoppingCart'  is null.");
            }
            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCart.Remove(shoppingCart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(int id)
        {
          return (_context.ShoppingCart?.Any(e => e.RecordId == id)).GetValueOrDefault();
        }


        public IActionResult AddToCart(List<int> selectedSongs)
        {
            //try get value from cookie
            HttpContext.Request.Cookies.TryGetValue("UserId", out string? cartId);

            if (cartId != null)
            {
                foreach (int songId in selectedSongs)
                {
                    // Check if song is already in cart.
                    ShoppingCart? result = (from cart in _context.ShoppingCart
                                            where cart.UserId == Convert.ToInt32(cartId)
                                            where cart.SongId == songId
                                            select cart).FirstOrDefault();

                    if (result != null)
                    {
                        result.Count++;
                        _context.ShoppingCart.Update(result);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Create new cart object
                        _context.ShoppingCart.Add(new ShoppingCart() { UserId = Convert.ToInt32(cartId), Count = 1, SongId = songId });
                        _context.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index", "Checkout");
        }
    }
}
