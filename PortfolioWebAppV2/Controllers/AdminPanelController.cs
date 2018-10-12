using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;

namespace PortfolioWebAppV2.Controllers
{
    public class AdminPanelController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditContact()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ContactViewModel contactViewModel = Mapper.Map<Contact, ContactViewModel>(db.Contacts.FirstOrDefault());

            return View(contactViewModel);
        }

        [HttpPost]
        public ActionResult UpdateContact(ContactViewModel contactViewModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Contact contact = db.Contacts.FirstOrDefault();

            if (contact != null)
            {
                var c = Mapper.Map<ContactViewModel, Contact>(contactViewModel);
                contact.PhoneNumber = c.PhoneNumber;
                contact.Email1 = c.Email1;
                contact.Email2 = c.Email2;
                contact.FacebookLink = c.FacebookLink;
                contact.GitHubLink = c.GitHubLink;
                contact.LinkedInLink = c.LinkedInLink;

                db.SaveChanges();
            }

            return RedirectToAction("EditContact");
        }
    }
}