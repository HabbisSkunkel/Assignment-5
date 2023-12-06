using Microsoft.AspNetCore.Mvc;
using MusicShop.Data;

namespace MusicShop.Controllers
{
    public class LogonController : Controller
    {
        // Add context to database
        private readonly MusicShopContext _context;

        public LogonController(MusicShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string Username, string Password)
        {
            // Check if user exists
            var result = (from user in _context.OnlineUser
                         where user.UserName == Username && user.Password == Password
                         select user).FirstOrDefault();

            if (result != null)
            {
                // Store Username, UserId, and UserType in cookies
                HttpContext.Response.Cookies.Append("UserName", result.UserName);
                HttpContext.Response.Cookies.Append("UserId", result.UserId.ToString());
                HttpContext.Response.Cookies.Append("UserType", result.UserType);

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Not a valid login - redirect to login page
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
