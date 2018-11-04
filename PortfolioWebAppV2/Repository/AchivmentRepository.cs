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

        public void Add(Achievement entity)
        {
            Context.Achievements.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Achievement entity)
        {
            var obj = Context.Achievements.First(a => a.AchievementId == entity.AchievementId);
            Context.Achievements.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(Achievement entity)
        {
            try
            {
                var achievement = Context.Achievements.Single(a => a.AchievementId == entity.AchievementId) ?? throw new Exception("Not found");
                achievement.Date = entity.Date;
                achievement.Description = entity.Description;
                achievement.ShowInCv = entity.ShowInCv;
                achievement.Title = entity.Title;
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