using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.ModelBinding;
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

           // return Context.Achievements.FirstOrDefault(a => a.AchievementId == id);
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
            /* Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
             bool saveFailed;
             do
             {
                 saveFailed = false;

                 try
                 {
                     Context.SaveChanges();
                 }
                 catch (DbUpdateConcurrencyException ex)
                 {
                     saveFailed = true;

                     // Update the values of the entity that failed to save from the store
                     ex.Entries.Single().Reload();
                 }

             } while (saveFailed);
             */
            var achievement = Context.Achievements.Single(a => a.AchievementId == entity.AchievementId);
            achievement.Date = entity.Date;
            achievement.Description = entity.Description;
            achievement.ShowInCv = entity.ShowInCv;
            achievement.Title = entity.Title;
            Context.SaveChanges();

            return true;
        }
    }
}