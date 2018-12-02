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

        public bool Add(EmploymentHistory entity)
        {
            Context.EmploymentHistories.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(EmploymentHistory entity)
        {
            try
            {
                EmploymentHistory obj = Context.EmploymentHistories.First(a => a.EmploymentHistoryId == entity.EmploymentHistoryId);
                Context.EmploymentHistories.Remove(obj);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Update(EmploymentHistory entity)
        {
            try
            {
                EmploymentHistory employmentHistory = Context.EmploymentHistories.Single(a => a.EmploymentHistoryId == entity.EmploymentHistoryId) ?? throw new Exception($"Not found id: {entity.EmploymentHistoryId}");
                employmentHistory.CityOfEmployment = entity.CityOfEmployment;
                employmentHistory.CompanyName = entity.CompanyName;
                employmentHistory.CurrentPlaceOfEmployment = entity.CurrentPlaceOfEmployment;
                employmentHistory.EndDate = entity.EndDate;
                employmentHistory.Position = entity.Position;
                employmentHistory.StartDate = entity.StartDate;
                employmentHistory.ShowInCv = entity.ShowInCv;

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