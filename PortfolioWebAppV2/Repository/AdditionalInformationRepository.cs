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

        public IEnumerable<AdditionalInfo> GetAll()
        {
            return Context.AdditionalInfos.ToList();
        }

        public AdditionalInfo Get(int id)
        {
            return Context.AdditionalInfos.Find(id);
        }

        public void Add(AdditionalInfo entity)
        {
            Context.AdditionalInfos.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(AdditionalInfo entity)
        {
            var obj = Context.AdditionalInfos.Find(entity.AdditionalInfoId);
            Context.AdditionalInfos.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(AdditionalInfo entity)
        {
            AdditionalInfo additionalInfo = Context.AdditionalInfos.Find(entity.AdditionalInfoId);
            if (additionalInfo == null) return false;

            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
    }
}