using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Achievement
    {
        public int AchievementId { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić tytuł.")]
        [Display(Name = "Tytuł osiągnięcia")]
        public string Title { get; set; }

        [Display(Name = "Opis osiągnięcia")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić date.")]
        [Display(Name = "Data osiągnięcia")]
        public DateTime? Date { get; set; }

        [Display(Name = "Dodaj do CV")]
        public bool ShowInCv { get; set; }
    }
}