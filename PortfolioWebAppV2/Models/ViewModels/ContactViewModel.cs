using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.ViewModels
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }

        [Display(Name = "Link do profilu na portalu GitHub")]
        [Required(ErrorMessage = "Link do profilu na githuba jest wymagany")]
        public string GitHubLink { get; set; }

        [Display(Name = "Link do profilu na portali LinkedIn")]
        [Required(ErrorMessage = "Link do profilu na LinkedIn jest wymagany")]
        public string LinkedInLink { get; set; }

        [Display(Name = "Link do profilu na portalu Facebook")]
        public string FacebookLink { get; set; }

        [Display(Name = "Głowny adres e-mail")]
        [Required(ErrorMessage = "Głowny adres e-mail jest wymagany")]
        public string Email1 { get; set; }

        [Display(Name = "Pomocniczy adres e-mail")]
        public string Email2 { get; set; }

        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
    }
}