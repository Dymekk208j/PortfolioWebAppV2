﻿using System.Collections.Generic;

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
        public string Name { get; set; }
        public bool ShowInCv { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}