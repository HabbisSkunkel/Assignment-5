using Microsoft.AspNetCore.Mvc;
using MusicShop.Models;

namespace MusicShop.Controllers
{
    public class BrowseMusic : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
