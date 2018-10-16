using System.Collections.Generic;
using PortfolioWebAppV2.Models.DatabaseModels;

namespace PortfolioWebAppV2.Models.ViewModels
{
    public class ProjectsViewModel
    {
        public ICollection<Project> Projects { get; set; }
        public int Page { get; set; }
        public bool Commercial { get; set; }
    }
}