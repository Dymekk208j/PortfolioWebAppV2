﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Tytuł projektu nie może być pustu")]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "Skrócony opis")]
        [Required(ErrorMessage = "Skrócony opis nie może być pusty")]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [Display(Name = "Pełen opis")]
        [Required(ErrorMessage = "Pełen opis nie może być pusty")]
        public string FullDescription { get; set; }

        [Display(Name = "Projekt komercyjny")]
        public bool Commercial { get; set; }

        [Display(Name = "Pokazuj w CV")]
        public bool ShowInCv { get; set; }

        [Display(Name = "Ikona")]
        public Image Icon { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime DateTimeCreated { get; set; }

        [Display(Name = "Id autora")]
        public string AuthorId { get; set; }

        [Display(Name = "Projekt tymczasowy")]
        public bool TempProject { get; set; }

        [Display(Name = "GitHub link")]
        public string GitHubLink { get; set; }

        public virtual List<Technology> Technologies { get; set; }
        public virtual List<Image> Images { get; set; }
    }
}