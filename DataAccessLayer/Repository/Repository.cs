using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly appDbContext appDbContext;
        private readonly DbSet<T> dbSet;

        public Repository(appDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            dbSet = appDbContext.Set<T>();
            appDbContext.Articles
            .Include(a => a.User) // Include the User navigation property
            .Include(a => a.Comments) // Include the Comments navigation property
            .ToList();
            appDbContext.Comments.Include(a => a.User).ToList();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
