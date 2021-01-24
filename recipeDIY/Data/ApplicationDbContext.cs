using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using recipeDIY.Models;

namespace recipeDIY.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        override 
        public DbSet<ApplicationUser> Users { get; set; }
        
        public DbSet<Recipe> Recipes { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
