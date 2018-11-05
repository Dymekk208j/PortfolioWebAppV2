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

        public PrivateInformationRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<PrivateInformation> GetAll()
        {
            return Context.PrivateInformations.ToList();
        }

        public PrivateInformation Get(int id)
        {
            return Context.PrivateInformations.First(a => a.PrivateInformationId == id) ?? throw new InvalidOperationException();
        }

        public bool Add(PrivateInformation entity)
        {
            Context.PrivateInformations.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(PrivateInformation entity)
        {
            var obj = Context.PrivateInformations.First(a => a.PrivateInformationId == entity.PrivateInformationId);
            if (obj == null) return false;
            Context.PrivateInformations.Remove(obj);
            return Context.SaveChanges() > 0;
        }

        public bool Update(PrivateInformation entity)
        {
            try
            {
                var privateInformation = Context.PrivateInformations.Single(a => a.PrivateInformationId == entity.PrivateInformationId) ?? throw new Exception($"Not found id: {entity.PrivateInformationId}");
                privateInformation.City = entity.City;
                privateInformation.Email = entity.Email;
                privateInformation.FirstName = entity.FirstName;
                privateInformation.FlatNumber = entity.FlatNumber;
                privateInformation.HomePage = entity.HomePage;
                privateInformation.HouseNumber = entity.HouseNumber;
                privateInformation.ImageLink = entity.ImageLink;
                privateInformation.LastName = entity.LastName;
                privateInformation.PhoneNumber = entity.PhoneNumber;
                privateInformation.PostCode = entity.PostCode;
                privateInformation.Street = entity.Street;

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