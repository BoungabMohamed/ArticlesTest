using BuiesnesLogic.Entities;
using DataAccessLayer.Data;
using DataAccessLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ArticleRepository : Repository<Article> , IArticleRepository
    {
        private readonly appDbContext _appDbContext;
        public ArticleRepository(appDbContext app) : base(app) 
        {
            this._appDbContext = app;       
        }

        public Article? Get(int id)
        {
            return _appDbContext.Articles.Include(c => c.Comments).Include(c => c.User).FirstOrDefault(p => p.Id == id); 
        }

        public IEnumerable<Article> PostesBy(string id)
        {
            return _appDbContext.Articles.Include(c => c.Comments).Include(c => c.User).Where(p => p.UserId == id).ToList();
        }

        public void Update(Article article)
        {
            _appDbContext.Articles.Update(article);
        }

    }
}
