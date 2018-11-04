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

        public AboutMeRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<AboutMe> GetAll()
        {
            return Context.AboutMe.ToList();
        }

        public AboutMe Get(int id)
        {
            return Context.AboutMe.First(a => a.AboutMeId == id) ?? throw new InvalidOperationException();
        }

        public void Add(AboutMe entity)
        {
            Context.AboutMe.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(AboutMe entity)
        {
            var obj = Context.AboutMe.First(a => a.AboutMeId == entity.AboutMeId);
            Context.AboutMe.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(AboutMe entity)
        {
            try
            {
                var aboutMe = Context.AboutMe.Single(a => a.AboutMeId == entity.AboutMeId) ?? throw new Exception("Not found");
                aboutMe.Title = entity.Title;
                aboutMe.ImageLink = entity.ImageLink;
                aboutMe.Text = entity.Text;
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