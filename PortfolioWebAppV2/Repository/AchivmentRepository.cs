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

        public AchievementRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public IEnumerable<Achievement> GetAll()
        {
            return Context.Achievements.ToList();
        }

        public Achievement Get(int id)
        {
            return Context.Achievements.First(a => a.AchievementId == id) ?? throw new InvalidOperationException();
        }

        public bool Add(Achievement entity)
        {
            Context.Achievements.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(Achievement entity)
        {
            var obj = Context.Achievements.First(a => a.AchievementId == entity.AchievementId);
            if (obj == null) return false;

            Context.Achievements.Remove(obj);
            return Context.SaveChanges() > 0;
        }

        public bool Update(Achievement entity)
        {
            try
            {
                var achievement = Context.Achievements.Single(a => a.AchievementId == entity.AchievementId) ?? throw new Exception($"Not found id: {entity.AchievementId}");
                achievement.Date = entity.Date;
                achievement.Description = entity.Description;
                achievement.ShowInCv = entity.ShowInCv;
                achievement.Title = entity.Title;
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