using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class EducationRepository : IRepository<Education, int>
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public EducationRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Education> GetAll()
        {
            return Context.Educations.ToList();
        }

        public Education Get(int id)
        {
            return Context.Educations.First(a => a.EducationId == id) ?? throw new InvalidOperationException();
        }

        public bool Add(Education entity)
        {
            Context.Educations.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(Education entity)
        {
            var obj = Context.Educations.First(a => a.EducationId == entity.EducationId);
            if (obj == null) return false;
            Context.Educations.Remove(obj);

            return Context.SaveChanges() > 0;
        }

        public bool Update(Education entity)
        {
            try
            {
                var education = Context.Educations.Single(a => a.EducationId == entity.EducationId) ?? throw new Exception($"Not found id: {entity.EducationId}");
                education.CurrentPlaceOfEducation = entity.CurrentPlaceOfEducation;
                education.Department = entity.Department;
                education.EndDate = entity.EndDate;
                education.SchooleName = entity.SchooleName;
                education.ShowInCv = entity.ShowInCv;
                education.Specialization = entity.Specialization;
                education.StartDate = education.StartDate;

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