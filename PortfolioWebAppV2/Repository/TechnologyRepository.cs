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

        public IEnumerable<Technology> Get()
        {
            return Context.Technologies.ToList();
        }

        public Technology Get(int id)
        {
            return Context.Technologies.Find(id);
        }

        public void Add(Technology entity)
        {
            Context.Technologies.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Technology entity)
        {
            var obj = Context.Technologies.Find(entity.TechnologyId);
            Context.Technologies.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(Technology entity)
        {
            Technology technology = Context.Technologies.Find(entity.TechnologyId);
            if (technology == null) return false;

            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
    }
}