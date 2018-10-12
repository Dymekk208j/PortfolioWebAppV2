using System;
using System.Collections.Generic;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string ShordDescription { get; set; }
        public string FullDescription { get; set; }
        public bool Commercial { get; set; }
        public bool ShowInCv { get; set; }
        public bool IsIcon { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string AuthorId { get; set; }
        public ICollection<Technology> Technologies { get; set; }
        public bool TempProject { get; set; }
    }
}