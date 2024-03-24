using BuiesnesLogic.Entities;
using DataAccessLayer.Data;
using DataAccessLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly appDbContext _appDbContext;
        public CommentRepository(appDbContext appDbContext) : base (appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public IEnumerable<Comment> CommentsBy(string id)
        {
            return _appDbContext.Comments.Where(p => p.UserId == id);
        }

        public Comment? Get(int id)
        {
            return _appDbContext.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Comment comment)
        { 
            _appDbContext.Comments.Update (comment);
        }
    }
}
