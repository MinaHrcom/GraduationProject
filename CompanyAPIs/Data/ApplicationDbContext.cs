using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CompanyAPIs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        
        { 
                
        }

        public DbSet<Operations> Operation { get; set; }
        public DbSet<Ships> Ship { get; set; }
        public DbSet<Documents> Document { get; set; }

       
    }
}
