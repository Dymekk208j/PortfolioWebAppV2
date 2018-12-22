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
            return Context.Technologies.Include("Projects").ToList();
        }

        public Technology Get(int id)
        {
            return Context.Technologies.Include("Projects").First(a => a.TechnologyId == id) ?? throw new InvalidOperationException();
        }

        public bool Add(Technology entity)
        {
            Context.Technologies.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(Technology entity)
        {
            try
            {
                Technology obj = Context.Technologies.First(a => a.TechnologyId == entity.TechnologyId);
                Context.Technologies.Remove(obj);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Update(Technology entity)
        {
            try
            {
                Technology achievement = Context.Technologies.Single(a => a.TechnologyId == entity.TechnologyId) ?? throw new Exception($"Not found id: {entity.TechnologyId}");
                achievement.KnowledgeLevel = entity.KnowledgeLevel;
                achievement.Name = entity.Name;
                achievement.ShowInCv = entity.ShowInCv;
                achievement.Projects = entity.Projects;

                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}