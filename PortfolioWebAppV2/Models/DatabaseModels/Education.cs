using System;
using System.ComponentModel.DataAnnotations;


namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Education
    {
        public int EducationId { get; set; }

        [Required(ErrorMessage = "Nazwa szkoły/uczelni nie może być pusta.")]
        [Display(Name = "Nazwa szkoły/uczelni")]
        public string SchooleName { get; set; }

        [Required(ErrorMessage = "Poziom szkoły/Wydział nie może być puste.")]
        [Display(Name = "Poziom szkoły/Wydział")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Specjalizacja nie może być pusta.")]
        [Display(Name = "Specjalizacja")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Data rozpoczęcia nie może być pusta.")]
        [Display(Name = "Data rozpoczęcia")]
        //[Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Data zakończenia")]
        //[Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Aktualne miejsce nauki")]
        public bool CurrentPlaceOfEducation { get; set; }

        [Display(Name = "Dodaj do CV")]
        public bool ShowInCv { get; set; }
    }
}