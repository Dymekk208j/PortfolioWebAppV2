
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class PrivateInformation
    {
        public int PrivateInformationId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Imię musi się składać od 3 do 50 znaków")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwisko musi się składać od 3 do 50 znaków")]
        public string LastName { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string HomePage { get; set; }
        public string Email { get; set; }
        public string ImageLink { get; set; }
    }
}