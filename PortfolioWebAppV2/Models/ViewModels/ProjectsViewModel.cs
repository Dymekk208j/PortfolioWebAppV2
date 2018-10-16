using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;

namespace PortfolioWebAppV2.Models.ViewModels
{
    public class ProjectsViewModel
    {
        public ICollection<Project> Projects { get; set; }
        public int Page { get; set; }
        public bool Commercial { get; set; }

        public bool IsNextPage()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Projects.Where(a => a.Commercial == Commercial).ToList().Count() / 10 > Page;
        }
        public bool IsPreviousPage()
        {
            return Page - 1 >= 0;
        }
    }
}