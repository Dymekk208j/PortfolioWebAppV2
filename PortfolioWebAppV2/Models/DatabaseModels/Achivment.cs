using System;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Achivment
    {
        public int AchivmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool ShowInCv { get; set; }
    }
}