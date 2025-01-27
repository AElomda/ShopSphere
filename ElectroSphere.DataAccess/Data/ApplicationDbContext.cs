using Microsoft.EntityFrameworkCore;
using ElectroSphere.Entities.Models;

namespace ElectroSphere.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
