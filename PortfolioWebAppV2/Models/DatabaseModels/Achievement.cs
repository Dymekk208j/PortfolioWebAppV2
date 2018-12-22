using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Achievement
    {
        public int AchievementId { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić tytuł.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tytuł musi się składać od 5 do 100 znaków")]
        [Display(Name = "Tytuł osiągnięcia")]
        public string Title { get; set; }

        [Display(Name = "Opis osiągnięcia")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Opis musi się składać od 5 do 200 znaków")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić date.")]
        [Display(Name = "Data osiągnięcia")]
        public DateTime Date { get; set; }

        [Display(Name = "Dodaj do CV")]
        public bool ShowInCv { get; set; }
    }
}