using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Image
    {
        public int ImageId { get; set; }
        [Required(ErrorMessage = "Nazwa pliku nie może być pusta")]
        public string FileName { get; set; }
        public string Link { get; set; }
        public Project Project { get; set; }
    }
}