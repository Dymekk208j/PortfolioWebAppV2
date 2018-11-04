using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class PrivateInformationRepository : IRepository<PrivateInformation, int>
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public IEnumerable<PrivateInformation> GetAll()
        {
            return Context.PrivateInformations.ToList();
        }

        public PrivateInformation Get(int id)
        {
            return Context.PrivateInformations.Find(id);
        }

        public void Add(PrivateInformation entity)
        {
            Context.PrivateInformations.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(PrivateInformation entity)
        {
            var obj = Context.PrivateInformations.Find(entity.PrivateInformationId);
            Context.PrivateInformations.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(PrivateInformation entity)
        {
            PrivateInformation privateInformation = Context.PrivateInformations.FirstOrDefault();
            if (privateInformation == null) return false;

            entity.PrivateInformationId = privateInformation.PrivateInformationId;
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
    }
}