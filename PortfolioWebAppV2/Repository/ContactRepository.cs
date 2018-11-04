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

        public IEnumerable<Contact> GetAll()
        {
            return Context.Contacts.ToList();
        }

        public Contact Get(int id)
        {
            return Context.Contacts.Find(id);
        }

        public void Add(Contact entity)
        {
            Context.Contacts.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Contact entity)
        {
            var obj = Context.Contacts.Find(entity.ContactId);
            Context.Contacts.Remove(obj ?? throw new InvalidOperationException());
            Context.SaveChanges();
        }

        public bool Update(Contact entity)
        {
            Contact contact = Context.Contacts.FirstOrDefault();
            if (contact == null) return false;

            entity.ContactId = contact.ContactId;
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
    }
}