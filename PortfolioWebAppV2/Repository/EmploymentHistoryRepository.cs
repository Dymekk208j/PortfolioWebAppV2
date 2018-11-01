using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class EmploymentHistoryRepository : IRepository<EmploymentHistory, int>
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public IEnumerable<EmploymentHistory> Get()
        {
            return Context.EmploymentHistories.ToList();
        }

        public EmploymentHistory Get(int id)
        {
            return Context.EmploymentHistories.Find(id);
        }

        public void Add(EmploymentHistory entity)
        {
            Context.EmploymentHistories.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(EmploymentHistory entity)
        {
            var obj = Context.EmploymentHistories.Find(entity.EmploymentHistoryId);
            Context.EmploymentHistories.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(EmploymentHistory entity)
        {
            EmploymentHistory employmentHistory = Context.EmploymentHistories.Find(entity.EmploymentHistoryId);
            if (employmentHistory == null) return false;

            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
    }
}