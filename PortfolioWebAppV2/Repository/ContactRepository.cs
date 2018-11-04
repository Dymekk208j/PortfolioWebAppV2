using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioWebAppV2.Models.DatabaseModels;
using Unity.Attributes;

namespace PortfolioWebAppV2.Repository
{
    public class ContactRepository : IRepository<Contact, int>
    {
        [Dependency]
        public ApplicationDbContext Context { get; set; }

        public ContactRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Contact> GetAll()
        {
            return Context.Contacts.ToList();
        }

        public Contact Get(int id)
        {
            return Context.Contacts.First(a => a.ContactId == id) ?? throw new InvalidOperationException();
        }

        public void Add(Contact entity)
        {
            Context.Contacts.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Contact entity)
        {
            var obj = Context.Contacts.First(a => a.ContactId == entity.ContactId);
            Context.Contacts.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(Contact entity)
        {
            try
            {
                var contact = Context.Contacts.Single(a => a.ContactId == entity.ContactId) ?? throw new Exception($"Not found id: {entity.ContactId}");
                contact.ContactId = entity.ContactId;
                contact.Email1 = entity.Email1;
                contact.Email2 = entity.Email2;
                contact.FacebookLink = entity.FacebookLink;
                contact.GitHubLink = entity.GitHubLink;
                contact.LinkedInLink = entity.LinkedInLink;
                contact.PhoneNumber = entity.PhoneNumber;
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