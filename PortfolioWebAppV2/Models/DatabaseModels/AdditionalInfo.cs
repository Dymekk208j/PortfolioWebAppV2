
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class AdditionalInfo
    {
        public enum TypeOfAddtionalInfo
        {
            [Display(Name = "Język obcy")]
            ForeignLanguages,
            [Display(Name = "Dodatkowa umiejętność")]
            AdditionalSkills,
            [Display(Name = "Zainteresowanie")]
            Interests
        }

        public int AdditionalInfoId { get; set; }

        [Display(Name = "Typ")]
        public TypeOfAddtionalInfo Type { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Dodaj do Cv")]
        public bool ShowInCv { get; set; }

    }
}