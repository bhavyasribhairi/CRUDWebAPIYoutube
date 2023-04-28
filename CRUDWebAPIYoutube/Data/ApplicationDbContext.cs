using CRUDWebAPIYoutube.Models;
using Microsoft.EntityFrameworkCore;
namespace CRUDWebAPIYoutube.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {        

        }
        public DbSet<Image> Images { get; set; }
        public DbSet<Details> Details { get; set; }
        public DbSet<Contact> Contacts { get; set; }

            

        //seeding data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Contact table
            var contactstoinsert = new List<Contact>() {

            new Contact()
            {
                Id= Guid.Parse("816f70bc-7f5a-4b7c-9ae0-0e3cc37db9a0"),
                FullName = "Ram",
                Email="ram@gmail.com",
                Phone=987238642942,
                Address="Old post office street"
            },
            new Contact()
            {
                Id= Guid.Parse("88bfe5d0-a564-4f08-a9e2-69cb1d2b94d3"),
                FullName = "Chand",
                Email="chandha@gmail.com",
                Phone=987238642942,
                Address="Rajahmundry"
            },
            new Contact()
            {
                Id= Guid.Parse("be001a26-3788-4b03-8320-704fadb48f3d"),
                FullName = "Mohit",
                Email="mohit@gmail.com",
                Phone=9872228642942,
                Address="Nuzvid"
            }
            };
            modelBuilder.Entity<Contact>().HasData(contactstoinsert);

            //Details Table
            var detailstoinsert = new List<Details>()
            {
                new Details()
                {
                    Id=Guid.Parse("0d9e233a-fd21-4eef-a27e-054e84c0c7f1"),
                    PanNumber= 123123123123,
                    AadharNumber= 345345345345,
                    ContactId =Guid.Parse("816F70BC-7F5A-4B7C-9AE0-0E3CC37DB9A0")
                },
                new Details()
                {
                    Id=Guid.Parse("1f087157-1c26-447f-af4d-06d1676824b9"),
                    PanNumber= 567545464454,
                    AadharNumber= 345345345345,
                    ContactId =Guid.Parse("88bfe5d0-a564-4f08-a9e2-69cb1d2b94d3")

                },
                new Details()
                {
                    Id=Guid.Parse("42f3c01d-a632-4cb4-9bb1-b50514683381"),
                    PanNumber= 567545464454,
                    AadharNumber= 345345345345,
                    ContactId =Guid.Parse("be001a26-3788-4b03-8320-704fadb48f3d")
                }
                };
            modelBuilder.Entity<Details>().HasData(detailstoinsert);
        }


    }
}
