using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BuiesnesLogic.Entities; // Ensure this using directive is present to access your entities

namespace DataAccessLayer.Data
{
    public class appDbContext : IdentityDbContext<ApplicationUser>
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ApplicationUser configuration
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.Firstname).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Lastname).IsRequired().HasMaxLength(50);
                // Image and Bio are allowed to be null by default, so no need to configure them here
            });

            // Article configuration
            builder.Entity<Article>(entity =>
            {
                entity.Property(a => a.Title).IsRequired().HasMaxLength(200);
                entity.Property(a => a.SubTitle).IsRequired(); // Assuming "max length max" means no explicit limit
                entity.Property(a => a.Content).IsRequired(); // Assuming "max length max" means no explicit limit
                entity.Property(a => a.Image).IsRequired();
                entity.Property(a => a.PostDate).IsRequired();

                entity.HasOne(a => a.User)
                      .WithMany(u => u.Articles)
                      .HasForeignKey(a => a.UserId);

                entity.HasMany(a => a.Comments)
                      .WithOne(c => c.Article)
                      .HasForeignKey(c => c.ArticleId);
            });

            // Comment configuration
            builder.Entity<Comment>(entity =>
            {
                entity.Property(c => c.Content).IsRequired(); // Assuming "max length max" means no explicit limit
                entity.Property(c => c.PostDate).IsRequired();

                entity.HasOne(c => c.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId);

                entity.HasOne(c => c.Article)
                      .WithMany(a => a.Comments)
                      .HasForeignKey(c => c.ArticleId);
            });
        }
    }
}

