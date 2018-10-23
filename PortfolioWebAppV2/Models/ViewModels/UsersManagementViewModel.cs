using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortfolioWebAppV2.Models.DatabaseModels;

namespace PortfolioWebAppV2.Models.ViewModels
{
    public class UsersManagementViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public string UserNameFilters { get; set; }
        public string FirstNameFilters { get; set; }
        public string LastNameFilters { get; set; }
        public string EmailFilters { get; set; }
        public bool? BlockedFilter { get; set; }
        public int? Page { get; set; }

        public int MaxPage()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var usersList = db.Users.OrderBy(a => a.UserName).ToList();

            if (!string.IsNullOrEmpty(UserNameFilters)) usersList = usersList.Where(d => d.UserName == UserNameFilters).ToList();
            if (!string.IsNullOrEmpty(FirstNameFilters)) usersList = usersList.Where(d => d.FirstName == FirstNameFilters).ToList();
            if (!string.IsNullOrEmpty(LastNameFilters)) usersList = usersList.Where(d => d.LastName == LastNameFilters).ToList();
            if (!string.IsNullOrEmpty(EmailFilters)) usersList = usersList.Where(d => d.Email == FirstNameFilters).ToList();
            if (BlockedFilter != null) usersList = usersList.Where(d => d.Blocked == BlockedFilter).ToList();

            int result = usersList.ToList().Count / 10;

            return result;
        }
    }
}