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

        public bool Add(AboutMe entity)
        {
            Context.AboutMe.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(AboutMe entity)
        {
            try
            {
                AboutMe obj = Context.AboutMe.First(a => a.AboutMeId == entity.AboutMeId);
                Context.AboutMe.Remove(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Update(AboutMe entity)
        {
            try
            {
                AboutMe aboutMe = Context.AboutMe.Single(a => a.AboutMeId == entity.AboutMeId) ?? throw new Exception("Not found");
                aboutMe.Title = entity.Title;
                aboutMe.ImageLink = entity.ImageLink;
                aboutMe.Text = entity.Text;
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