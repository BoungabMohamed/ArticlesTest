using BuiesnesLogic.Entities;
using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface ICommentRepository : IRepository <Comment>
    {
        Comment? Get (int id);
        void Update (Comment comment);
        IEnumerable<Comment> CommentsBy(string id);
    }
}
