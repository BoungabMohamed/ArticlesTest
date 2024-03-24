using BuiesnesLogic.Entities;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IArticleRepository : IRepository <Article>
    {
        Article? Get (int id);
        void Update (Article article);
        IEnumerable<Article> PostesBy(string id);
    }
}
