using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class AboutMe
    {
        public int AboutMeId { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Text { get; set; }

        [Required]
        public string ImageLink { get; set; }
    }
}