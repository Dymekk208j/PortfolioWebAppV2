using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Technology
    {
        public enum LevelOfKnowledge
        {
            VeryWell,
            Well,
            Ok
        }
        public int TechnologyId { get; set; }
        public LevelOfKnowledge KnowledgeLevel { get; set; }

        [Required]
        [Display(Name = "Nazwa technologii")]
        public string Name { get; set; }
        public bool ShowInCv { get; set; }

        [JsonIgnore] 
        [IgnoreDataMember] 
        public ICollection<Project> Projects { get; set; }
    }
}
