using Microsoft.AspNetCore.Mvc.Filters;

namespace MusicShop
{
    public sealed class AdminAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if user is admin
            context.HttpContext.Request.Cookies.TryGetValue("UserType", out string? userType);

            if (userType != "admin")
            {
                // redirect to login
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectToActionResult("Index", "Logon", null);
            }
        }
    }
}
