using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShop.Data;
using MusicShop.Models;

namespace MusicShop.Controllers
{
    public class BrowseMusicController : Controller
    {
        private readonly MusicShopContext _context;

        public BrowseMusicController(MusicShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreName");
            return View();
        }


    }
}
