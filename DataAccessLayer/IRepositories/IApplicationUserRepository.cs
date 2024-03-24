using BuiesnesLogic.Entities;
using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IApplicationUserRepository : IRepository <ApplicationUser>
    {
        ApplicationUser? Get(string id);
        void Update (ApplicationUser user);
    }
}
