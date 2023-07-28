using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestSiteKursova.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RestSiteKursova.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "user", NormalizedName = "USER" },
                new IdentityRole { Name = "admin", NormalizedName = "ADMIN", Id = "eem24nas-sadmmma-sf3" },
                new IdentityRole { Name = "courier", NormalizedName = "COURIER" }
                );

            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                NormalizedEmail="SUPERUSER@GMAIL.COM",
                NormalizedUserName="SUPERUSER@GMAIL.COM",
                Id = "sadmoment-history-superuser-id",
                UserName = "SuperUser@gmail.com",
                Email = "SuperUser@gmail.com",
                PasswordHash = hasher.HashPassword(null, "SuperUser")
            }
        );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {

                RoleId = "eem24nas-sadmmma-sf3",
                UserId = "sadmoment-history-superuser-id"
            }
        );
        }
    }
}