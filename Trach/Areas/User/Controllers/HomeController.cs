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
        public IActionResult UpdateIformation(string firstname, string lastname, string email, string bio)
        {

            var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();


            user!.Firstname = firstname;
            user!.Lastname = lastname;
            user!.Email = email;
            user!.Bio = bio;

            userManager.UpdateAsync(user).GetAwaiter().GetResult();


            return View("Index", user);
        }

    }
}
