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
            // Clear cookies
            HttpContext.Response.Cookies.Delete("CartId");
            HttpContext.Response.Cookies.Delete("UserType");

            return View();
        }

        public IActionResult Login(string Username, string Password)
        {
            var result = (from user in _context.OnlineUser
                         where user.UserName == Username && user.Password == Password
                         select user).FirstOrDefault();

            if (result != null)
            {
                // Check for admin
                if(result.UserName =="admin")
                {
                    // Store admin type and CartId in cookies
                    HttpContext.Response.Cookies.Append("UserType", "admin");
                    HttpContext.Response.Cookies.Append("CartId", result.CartId.ToString());

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    // Store user type and CartId in cookies
                    HttpContext.Response.Cookies.Append("UserType", "user");
                    HttpContext.Response.Cookies.Append("CartId", result.CartId.ToString());

                    return RedirectToAction("Index", "BrowseMusic");
                }
            }

            // Not a valid login
            return RedirectToAction(nameof(Index));
        }
    }
}
