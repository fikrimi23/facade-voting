using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<App.Models.Category> Category { get; set; }

        public DbSet<App.Models.Feature> Feature { get; set; }
    }
}