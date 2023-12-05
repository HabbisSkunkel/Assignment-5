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
            var result = (from user in _context.OnlineUser
                         where user.UserName == Username && user.Password == Password
                         select user).FirstOrDefault();

            if (result != null)
            {
                // Check for admin
                if(result.UserName =="admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "BrowseMusic");
                }
            }

            // Not a valid login
            return RedirectToAction(nameof(Index));
        }
    }
}
