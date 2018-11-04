using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class AdditionalInformationRepository : IRepository<AdditionalInfo, int>
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public AdditionalInformationRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<AdditionalInfo> GetAll()
        {
            return Context.AdditionalInfos.ToList();
        }

        public AdditionalInfo Get(int id)
        {
            return Context.AdditionalInfos.First(a => a.AdditionalInfoId == id) ?? throw new InvalidOperationException();
        }

        public void Add(AdditionalInfo entity)
        {
            Context.AdditionalInfos.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(AdditionalInfo entity)
        {
            var obj = Context.AdditionalInfos.First(a => a.AdditionalInfoId == entity.AdditionalInfoId);
            Context.AdditionalInfos.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(AdditionalInfo entity)
        {
            try
            {
                var additionalInfo = Context.AdditionalInfos.Single(a => a.AdditionalInfoId == entity.AdditionalInfoId) ?? throw new Exception("Not found");
                additionalInfo.Title = entity.Title;
                additionalInfo.ShowInCv = entity.ShowInCv;
                additionalInfo.Type = entity.Type;

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