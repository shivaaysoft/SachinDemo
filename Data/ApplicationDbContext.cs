using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectAgreementManagement.Models;

namespace ProjectAgreementManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Agreement> Agreement { get; set; }
    }
}
