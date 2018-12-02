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

        public bool Add(Contact entity)
        {
            Context.Contacts.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Remove(Contact entity)
        {
            try
            {
                Contact obj = Context.Contacts.First(a => a.ContactId == entity.ContactId);
                Context.Contacts.Remove(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return Context.SaveChanges() > 0;
        }

        public bool Update(Contact entity)
        {
            try
            {
                Contact contact = Context.Contacts.Single(a => a.ContactId == entity.ContactId) ?? throw new Exception($"Not found id: {entity.ContactId}");
                contact.ContactId = entity.ContactId;
                contact.Email1 = entity.Email1;
                contact.Email2 = entity.Email2;
                contact.FacebookLink = entity.FacebookLink;
                contact.GitHubLink = entity.GitHubLink;
                contact.LinkedInLink = entity.LinkedInLink;
                contact.PhoneNumber = entity.PhoneNumber;

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