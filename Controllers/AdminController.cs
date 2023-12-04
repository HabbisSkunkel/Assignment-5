using Microsoft.AspNetCore.Mvc;

namespace MusicShop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
