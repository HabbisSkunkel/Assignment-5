using Microsoft.AspNetCore.Mvc;

namespace MusicShop.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            // Clear cookies
            HttpContext.Response.Cookies.Delete("UserId");
            HttpContext.Response.Cookies.Delete("UserType");
            HttpContext.Response.Cookies.Delete("UserName");

            return View();
        }
    }
}
