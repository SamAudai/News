using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Shared.Models;

namespace News.Server.Models
{
    public class NewsDbContext : IdentityDbContext<ApplicationUser>
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<NewsList> NewsLists { get; set; }

        public DbSet<Category> Categoies { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
