using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiesnesLogic.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }

        // Relationship properties
        public string UserId { get; set; } // Change to string to match IdentityUser's ID type
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
