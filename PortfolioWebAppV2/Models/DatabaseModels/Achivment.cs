using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Achivment
    {
        public int AchivmentId { get; set; }

        [Display(Name = "Tytuł osiągnięcia")]
        public string Title { get; set; }

        [Display(Name = "Opis osiągnięcia")]
        public string Description { get; set; }

        [Display(Name = "Data osiągnięcia")]
        public DateTime Date { get; set; }

        [Display(Name = "Dodaj co CV")]
        public bool ShowInCv { get; set; }
    }
}