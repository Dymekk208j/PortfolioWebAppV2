using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class ContactController : Controller
    {
        private IRepository<Contact, int> _repository;

        public ContactController(IRepository<Contact, int> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public ActionResult ContactManagement()
        {
            var contact = _repository.GetAll().FirstOrDefault();
            ContactViewModel contactViewModel = Mapper.Map<Contact, ContactViewModel>(contact);

            return View(contactViewModel);
        }

        [HttpPost]
        public ActionResult Update(ContactViewModel contactViewModel)
        {

            var contact = Mapper.Map<ContactViewModel, Contact>(contactViewModel);
            _repository.Update(contact);

            return RedirectToAction("ContactManagement");
        }

        [HttpGet]
        public ActionResult Contact()
        {
            Contact contact = _repository.GetAll().FirstOrDefault();
            var contactViewModel = Mapper.Map<Contact, ContactViewModel>(contact);

            return View(contactViewModel);
        }
    }
}