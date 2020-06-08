using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                NormalizedName = Role.Admin.ToUpper(),
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Role.User,
                Name = Role.User,
                NormalizedName = Role.User.ToUpper(),
            });
        }
    }
}
