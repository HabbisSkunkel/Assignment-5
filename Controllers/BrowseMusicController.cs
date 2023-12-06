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
        public IActionResult Index(int? genreId, int? artistId)
        {
            // Check if user is logged in
            HttpContext.Request.Cookies.TryGetValue("UserType", out string? userType);

            if (userType != null)
            {
                IQueryable<Song> filteredSongs;
                IQueryable<Artist> filteredArtists;

                if (genreId == null && artistId == null) //All songs
                {
                    filteredSongs = _context.Song;
                    filteredArtists = _context.Artist;
                }
                else if (genreId != null && artistId == null) //Filter by genre only
                {
                    filteredSongs = _context.Song.Where(s => s.GenreId == genreId);
                    filteredArtists = (from s in filteredSongs
                                       from a in _context.Artist
                                       where s.ArtistId == a.ArtistId
                                       select a).Distinct();
                }
                else if (genreId == null && artistId != null) //Filter by artist only
                {
                    filteredSongs = _context.Song.Where(s => s.ArtistId == artistId);
                    filteredArtists = _context.Artist;
                }
                else //Filter by genre and artist
                { 
                    filteredArtists = (from s in _context.Song
                                        from a in _context.Artist
                                        where s.GenreId == genreId
                                        where s.ArtistId == a.ArtistId
                                        select a).Distinct();
                    filteredSongs = _context.Song.Where(s => s.GenreId == genreId && s.ArtistId == artistId);
                }
                ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreName", genreId);
                ViewData["ArtistId"] = new SelectList(filteredArtists, "ArtistId", "ArtistName");
                //Replace with list
                var items = new List<int>();
                ViewData["SongList"] = new MultiSelectList(filteredSongs, "SongId", "Title");
                return View(filteredSongs);
            }
            else
            {
                // redirect to login
                return RedirectToAction("Index", "Logon");
            }

            
        }

        public IActionResult AddToCart()
        {
            return RedirectToAction("AddToCart", "ShoppingCarts", 1);
        }
    }
}
