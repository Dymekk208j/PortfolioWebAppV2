using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class AchievementRepository : IRepository<Achievement, int>
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public IEnumerable<Achievement> Get()
        {
            return Context.Achievements.ToList();
        }

        public Achievement Get(int id)
        {
            return Context.Achievements.Find(id);
        }

        public void Add(Achievement entity)
        {
            Context.Achievements.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Achievement entity)
        {
            var obj = Context.Achievements.Find(entity.AchievementId);
            Context.Achievements.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(Achievement entity)
        {
            Achievement achievement = Context.Achievements.Find(entity.AchievementId);
            if (achievement == null) return false;

            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
    }
}