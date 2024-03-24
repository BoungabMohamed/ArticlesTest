using DataAccessLayer.IRepositories;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trach.Utility;

namespace Trach.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index ()
        {
            return View();
        }
        public IActionResult ShowAllUsers()
        {
            var Users = unitOfWork.ApplicationUser.GetAll();
            return View(Users);
        }
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var Comment = unitOfWork.Comment.CommentsBy(id);
            foreach (var Post in Comment)
            {
                unitOfWork.Comment.Remove(Post);
            }
            var Postes = unitOfWork.Article.PostesBy(id);
            foreach (var Post in Postes)
            {
                unitOfWork.Article.Remove (Post);
            }
            
            var u = unitOfWork.ApplicationUser.Get(id);
            if (u != null)
            {
                unitOfWork.ApplicationUser.Remove(u);
                unitOfWork.Save();
            }
        
            return View ("ShowAllUsers" , unitOfWork.ApplicationUser.GetAll());
        }
    }
}
