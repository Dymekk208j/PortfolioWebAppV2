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

        public EmploymentHistoryRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public IEnumerable<EmploymentHistory> GetAll()
        {
            return Context.EmploymentHistories.ToList();
        }

        public EmploymentHistory Get(int id)
        {
            return Context.EmploymentHistories.First(a => a.EmploymentHistoryId == id) ?? throw new InvalidOperationException();
        }

        public void Add(EmploymentHistory entity)
        {
            Context.EmploymentHistories.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(EmploymentHistory entity)
        {
            var obj = Context.EmploymentHistories.First(a => a.EmploymentHistoryId == entity.EmploymentHistoryId);
            Context.EmploymentHistories.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(EmploymentHistory entity)
        {
            try
            {
                var employmentHistory = Context.EmploymentHistories.Single(a => a.EmploymentHistoryId == entity.EmploymentHistoryId) ?? throw new Exception($"Not found id: {entity.EmploymentHistoryId}");
                employmentHistory.CityOfEmployment = entity.CityOfEmployment;
                employmentHistory.CompanyName = entity.CompanyName;
                employmentHistory.CurrentPlaceOfEmployment = entity.CurrentPlaceOfEmployment;
                employmentHistory.EndDate = entity.EndDate;
                employmentHistory.Position = entity.Position;
                employmentHistory.StartDate = entity.StartDate;
                employmentHistory.ShowInCv = entity.ShowInCv;
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