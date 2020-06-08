using App.Enum;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Identity.Data
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Feature> Feature { get; set; }

        public DbSet<UserVote> UserVote { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Role.Admin,
                Name = Role.Admin,
                NormalizedName = Role.Admin.ToUpper()
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Role.User,
                Name = Role.User,
                NormalizedName = Role.User.ToUpper()
            });

            builder.Entity<UserVote>()
                .HasKey(uv => new {uv.UserId, uv.FeatureId});

            builder.Entity<UserVote>()
                .HasOne(uv => uv.User)
                .WithMany(p => p.VotedFeatures)
                .HasForeignKey(pt => pt.UserId);

            builder.Entity<UserVote>()
                .HasOne(uv => uv.Feature)
                .WithMany(p => p.Voters)
                .HasForeignKey(pt => pt.FeatureId);
        }
    }
}