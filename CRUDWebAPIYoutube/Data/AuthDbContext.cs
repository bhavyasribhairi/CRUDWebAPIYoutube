using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebAPIYoutube.Data
{
    public class AuthDbContext : IdentityDbContext
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "e847b7ca-6a1f-4f0a-8cfb-01f012a460ab";
            var writerRoleId = "81fe5ea6-4a44-477c-91bd-f321623562bf";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= readerRoleId,
                    ConcurrencyStamp= readerRoleId,
                    Name= "Reader",
                    NormalizedName= "READER"
                },
                new IdentityRole
                {
                    Id= writerRoleId,
                    ConcurrencyStamp= writerRoleId,
                    Name= "Writer",
                    NormalizedName= "WRITER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
