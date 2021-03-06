﻿using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Castle.Core.Internal;
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
        [Authorize(Roles = "Admin")]
        public ActionResult ContactManagement()
        {
            Contact contact = _repository.GetAll().FirstOrDefault();
            ContactViewModel contactViewModel = Mapper.Map<Contact, ContactViewModel>(contact);

            return View(contactViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(ContactViewModel contactViewModel)
        {

            Contact contact = Mapper.Map<ContactViewModel, Contact>(contactViewModel);
            if (_repository.GetAll().IsNullOrEmpty()) _repository.Add(contact);
            _repository.Update(contact);

            return RedirectToAction("ContactManagement");
        }

        [HttpGet]
        public ActionResult Contact()
        {
            Contact contact = _repository.GetAll().FirstOrDefault();
            ContactViewModel contactViewModel = Mapper.Map<Contact, ContactViewModel>(contact);

            return View(contactViewModel);
        }
    }
}