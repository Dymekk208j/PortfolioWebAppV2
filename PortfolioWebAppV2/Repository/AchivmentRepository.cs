using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;
using Exception = System.Exception;

namespace PortfolioWebAppV2.Repository
{
    public class AchievementRepository : IRepository<Achievement, int>, IElementOfCv
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
            try
            {
                Achievement obj = Context.Achievements.First(a => a.AchievementId == entity.AchievementId);
                Context.Achievements.Remove(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Update(Achievement entity)
        {
            try
            {
                Achievement achievement = Context.Achievements.Single(a => a.AchievementId == entity.AchievementId) ?? throw new InvalidOperationException();
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

        public bool ChangeStatusInCv(int id)
        {
            try
            {
                Achievement achievement = Context.Achievements.Single(a => a.AchievementId == id);
                achievement.ShowInCv = !achievement.ShowInCv;
                return Update(achievement);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}