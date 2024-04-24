using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CompanyAPIs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        
        { 
        
        }

    }
}
