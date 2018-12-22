using System.Collections.Generic;
using PortfolioWebAppV2.Models.DatabaseModels;

namespace PortfolioWebAppV2.Models.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Technology> Technologies { get; set; }
        public IEnumerable<Project> Projects { get; set; }

    }
}