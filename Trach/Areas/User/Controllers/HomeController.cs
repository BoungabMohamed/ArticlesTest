using BuiesnesLogic.Entities;
using BuiesnesLogic.ModelView;
using DataAccessLayer.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trach.Utility;

namespace Trach.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
		public HomeController(IUnitOfWork unitOfWork , UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this.userManager = userManager;
		}
        public IActionResult Index()
        {
			var user =  userManager.GetUserAsync(User).GetAwaiter().GetResult();    
            UserArticlesMV userArticlesMV = new UserArticlesMV();
            userArticlesMV.user = user;
            userArticlesMV.Articles = null;
            
            if (User.IsInRole (Roles.Auther))
            {
                userArticlesMV.Articles = _unitOfWork.Article.PostesBy(user!.Id);
            }

            

			return View(userArticlesMV);
		}
        [HttpPost]
        public async Task<IActionResult> UpdateInformation(string firstname, string lastname, string email, string bio)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Firstname = firstname;
            user.Lastname = lastname;
            user.Email = email;
            // Manually set the NormalizedEmail. This is critical for the login process.
            user.NormalizedEmail = userManager.NormalizeEmail(email);
            user.NormalizedUserName = userManager.NormalizeName(email); 
            user.UserName = email;
            user.Bio = bio;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(); // Adjust as necessary based on your application.
            }

            return RedirectToAction("Index");
        }



    }
}
