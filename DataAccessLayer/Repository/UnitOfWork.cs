using DataAccessLayer.Data;
using DataAccessLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly appDbContext _appDbContext;
        public IApplicationUserRepository ApplicationUser { get;private set; }

        public IArticleRepository Article { get; private set; }

        public ICommentRepository Comment { get; private set; }

        public UnitOfWork(appDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            ApplicationUser = new ApplicationUserRepository(_appDbContext);
            Article = new ArticleRepository(_appDbContext);
            Comment = new CommentRepository(_appDbContext);
        }

        public void Save()
        {
           _appDbContext.SaveChanges();
        }
    }
}
