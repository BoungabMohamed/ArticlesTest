using BuiesnesLogic.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trach.Utility;

namespace Trach.Areas.Identity.Pages.Account
{
    [Area ("Identity")]
    public class RedirectController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public RedirectController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task< IActionResult> Index()
        {

            var user = await userManager.GetUserAsync(User);
           
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            else if (await userManager.IsInRoleAsync (user , Roles.Admin))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "User" });
            }
        }
    }
}
