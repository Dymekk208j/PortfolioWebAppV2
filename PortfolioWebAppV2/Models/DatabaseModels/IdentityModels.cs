﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
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

        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<Achivment> Achivments { get; set; }
        public DbSet<AdditionalInfo> AdditionalInfos { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public DbSet<PrivateInformation> PrivateInformations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Technology> Technologies{ get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}