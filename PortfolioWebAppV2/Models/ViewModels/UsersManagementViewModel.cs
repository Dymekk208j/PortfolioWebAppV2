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
    }
}