using BuiesnesLogic.Entities;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trach.Utility;

namespace Trach.Areas.User.Controllers
{
    [Area("User")]
    public class ArticleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager <ApplicationUser> userManager;
        public ArticleController(IUnitOfWork unitOfWork , UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        [Authorize(Roles = Roles.Auther)]
        public IActionResult Index()
        {
           return View();
        }

        [Authorize(Roles = Roles.Auther)]
        [HttpPost]
        public IActionResult Add (IFormFile image , string title , string subTitle , string content)
        {
            Article article = new Article();

            article.Title = title;
            article.SubTitle = subTitle;
            article.Content = content;
            article.PostDate = DateTime.Now;
            article.UserId = userManager.GetUserId(User);
            article.Image = Converter.IFormFileToImage (image);
           
            unitOfWork.Article.Add(article);
            unitOfWork.Save();
            return View("Succes");
        }
        public IActionResult Postes()
        {
            var postes = unitOfWork.Article.GetAll();
            return View("Postes", postes);
        }


        [HttpGet]
        public IActionResult GetPost(int id)
        {
            var p = unitOfWork.Article.Get(id);
            return View("GetPost", p);
        }
       

        [HttpGet]
        public IActionResult PostesBy(string id)
        {
            var result = unitOfWork.Article.PostesBy(id);
            return View("Postes", result);
        }


        [HttpPost]
        public IActionResult PostComment (string Content , int ArticleId)
        {

            if (User.Identity == null|| !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }


            Comment comment = new Comment();

            comment.Content = Content;
            comment.ArticleId = ArticleId;
            comment.PostDate = DateTime.Now;
            comment.UserId = userManager.GetUserId(User);


            unitOfWork.Comment.Add(comment);
            unitOfWork.Save();

            var p = unitOfWork.Article.Get(ArticleId);
            return View("GetPost", p);

        }


        [Authorize (Roles = Roles.Auther)]
        [HttpGet]
        /////////
        public IActionResult DeletePost (int id)
        {
            Article? article = unitOfWork.Article.Get (id);

            if (article != null)
            {
                foreach(var one in article.Comments.ToList())
                {
                    unitOfWork.Comment.Remove (one);
                }
                unitOfWork.Article.Remove (article);    
            }
            unitOfWork.Save();
            // save
            return RedirectToAction("Index", "Home", new { area = "User" });
        }
    }
}
