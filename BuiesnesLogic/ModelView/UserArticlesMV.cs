using BuiesnesLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiesnesLogic.ModelView
{
    public class UserArticlesMV
    {
        public ApplicationUser user { get; set; }
        public IEnumerable<Article> Articles { get; set; }  
    }
}