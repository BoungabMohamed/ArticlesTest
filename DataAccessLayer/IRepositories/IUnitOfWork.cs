using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IArticleRepository Article { get; }
        ICommentRepository Comment { get; }
        void Save();
    }
}
