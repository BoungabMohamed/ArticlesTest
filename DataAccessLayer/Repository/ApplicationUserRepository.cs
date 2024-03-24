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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly appDbContext _appDbContext;
        public ApplicationUserRepository(appDbContext appDbContext): base(appDbContext) 
        {
             this._appDbContext = appDbContext;
        }
        public ApplicationUser? Get(string id)
        {
            return _appDbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Update(ApplicationUser user)
        {
            _appDbContext.Users.Update(user);
        }
    }
}
