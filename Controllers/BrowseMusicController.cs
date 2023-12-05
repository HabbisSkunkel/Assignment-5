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
        public IActionResult Index(int genreId)
        {
            // Check if user is logged in
            HttpContext.Request.Cookies.TryGetValue("UserType", out string? userType);

            if (userType != null)
            {
                var filteredSongs = _context.Song.Where(s => s.GenreId == genreId);
                var filteredArtists = from s in filteredSongs
                                      from a in _context.Artist
                                      where s.ArtistId == a.ArtistId
                                      select a;

                ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreName", genreId);
                ViewData["ArtistId"] = new SelectList(filteredArtists, "ArtistId", "ArtistName");
                ViewData["SongList"] = new MultiSelectList(filteredSongs, "SongId", "Title");
                return View();
            }
            else
            {
                // redirect to login
                return RedirectToAction("Index", "Logon");
            }
        }
    }
}
