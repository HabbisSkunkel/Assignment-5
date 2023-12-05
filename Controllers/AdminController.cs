using Microsoft.AspNetCore.Mvc;

namespace MusicShop.Controllers
{
    [AdminAuthorizationFilter]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
