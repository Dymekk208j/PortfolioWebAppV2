using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class AboutMeRepository : IRepository<AboutMe, int>
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public IEnumerable<AboutMe> GetAll()
        {
            return Context.AboutMe.ToList();
        }

        public AboutMe Get(int id)
        {
            return Context.AboutMe.Find(id);
        }

        public void Add(AboutMe entity)
        {
            Context.AboutMe.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(AboutMe entity)
        {
            var obj = Context.AboutMe.Find(entity.AboutMeId);
            Context.AboutMe.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(AboutMe entity)
        {
            AboutMe aboutMe = Context.AboutMe.FirstOrDefault();
            if (aboutMe == null) return false;

            entity.AboutMeId = aboutMe.AboutMeId;
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
    }
}