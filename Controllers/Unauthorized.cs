using Microsoft.AspNetCore.Mvc;

namespace MusicShop.Controllers
{
    public class Unauthorized : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
