using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class EducationRepository : IRepository<Education, int>
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public IEnumerable<Education> Get()
        {
            return Context.Educations.ToList();
        }

        public Education Get(int id)
        {
            return Context.Educations.Find(id);
        }

        public void Add(Education entity)
        {
            Context.Educations.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Education entity)
        {
            var obj = Context.Educations.Find(entity.EducationId);
            Context.Educations.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(Education entity)
        {
            Education education = Context.Educations.Find(entity.EducationId);
            if (education == null) return false;

            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
    }
}