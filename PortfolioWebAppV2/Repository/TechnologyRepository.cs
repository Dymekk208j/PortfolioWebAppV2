using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class TechnologyRepository : IRepository<Technology, int>
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public TechnologyRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Technology> GetAll()
        {
            return Context.Technologies.ToList();
        }

        public Technology Get(int id)
        {
            return Context.Technologies.First(a => a.TechnologyId == id) ?? throw new InvalidOperationException();
        }

        public void Add(Technology entity)
        {
            Context.Technologies.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Technology entity)
        {
            var obj = Context.Technologies.First(a => a.TechnologyId == entity.TechnologyId) ?? throw new InvalidOperationException();
            Context.Technologies.Remove(obj);
            Context.SaveChanges();
        }

        public bool Update(Technology entity)
        {
            try
            {
                var achievement = Context.Technologies.Single(a => a.TechnologyId == entity.TechnologyId) ?? throw new Exception($"Not found id: {entity.TechnologyId}");
                achievement.KnowledgeLevel = entity.KnowledgeLevel;
                achievement.Name = entity.Name;
                achievement.ShowInCv = entity.ShowInCv;
                achievement.Projects = entity.Projects;
                Context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}