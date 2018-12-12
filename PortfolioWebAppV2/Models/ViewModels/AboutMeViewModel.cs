using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.ViewModels
{
    public class AboutMeViewModel
    {
        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Musisz wprowadzić tytuł")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Tytuł musi się składać od 5 do 50 znaków")]

        public string Title { get; set; }

        [Display(Name = "Treść")]
        [Required(ErrorMessage = "Musisz wprowadzić treść")]
        [StringLength(1500, MinimumLength = 10, ErrorMessage = "Treść musi się składać od 10 do 500 znaków")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Link do obrazka")]
        [Required(ErrorMessage = "Musisz wprowadzić link")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Link musi się składać od 5 do 200 znaków")]
        public string ImageLink { get; set; }
    }
}