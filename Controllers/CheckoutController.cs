using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShop.Data;
using MusicShop.Models;

namespace MusicShop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly MusicShopContext _context;

        public CheckoutController(MusicShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HttpContext.Request.Cookies.TryGetValue("CartId", out string? cartId);
            var musicShopContext = _context.ShoppingCart.Include(s => s.OnlineUser).Include(s => s.Song.Artist).Where(s => s.UserId == Convert.ToInt32(cartId)).OrderBy(s => s.Song.Artist.ArtistName).ThenBy(s => s.Song.Title);
            return View(await musicShopContext.ToListAsync());
        }
    }
}
