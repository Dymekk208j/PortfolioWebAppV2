using System.Collections.Generic;
using PortfolioWebAppV2.Models.DatabaseModels;

namespace PortfolioWebAppV2.Models.ViewModels
{
    public class CvViewModel
    {
        public int SelectedProject { get; set; }
        public int SelectedTechnology { get; set; }
        public int SelectedAchivment { get; set; }
        public int SelectedAddtinionaInformation { get; set; }
        public int SelectedEmploymentHistory { get; set; }
        public int SelectedEducation { get; set; }

        public Contact Contact { get; set; }
        public PrivateInformation PrivateInformation { get; set; }
        public List<Achivment> Achivments { get; set; }
        public List<AdditionalInfo> AdditionalInfos { get; set; }
        public List<Education> Educations { get; set; }
        public List<EmploymentHistory> EmploymentHistories { get; set; }
        public List<Project> Projects { get; set; }
        public List<Technology> Technologies { get; set; }
    }
}