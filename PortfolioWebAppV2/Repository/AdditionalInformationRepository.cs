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

        public bool Add(AdditionalInfo entity)
        {
            Context.AdditionalInfos.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(AdditionalInfo entity)
        {
            try
            {
                AdditionalInfo obj = Context.AdditionalInfos.First(a => a.AdditionalInfoId == entity.AdditionalInfoId);
                Context.AdditionalInfos.Remove(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Update(AdditionalInfo entity)
        {
            try
            {
                AdditionalInfo additionalInfo = Context.AdditionalInfos.Single(a => a.AdditionalInfoId == entity.AdditionalInfoId) ?? throw new Exception("Not found");
                additionalInfo.Title = entity.Title;
                additionalInfo.ShowInCv = entity.ShowInCv;
                additionalInfo.Type = entity.Type;

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