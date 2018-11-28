using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Zablokowany")]
        public bool Blocked { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AppConnectionString", throwIfV1Schema: false)
        {
            Database.CommandTimeout = 900;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<AboutMe> AboutMe { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<AdditionalInfo> AdditionalInfos { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public virtual DbSet<PrivateInformation> PrivateInformations { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Technology> Technologies { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

        public new virtual int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}