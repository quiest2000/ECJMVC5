using System.Data.Entity;
using ECommerce.Models.Domain.EfModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ECommerce.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MainDbConnection")
        {
            Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("QUINN");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}