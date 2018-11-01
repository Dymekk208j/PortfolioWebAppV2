﻿using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class AdditionalInformationController : Controller
    {
        private IRepository<AdditionalInfo, int> _repository;

        public AdditionalInformationController(IRepository<AdditionalInfo, int> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public ActionResult AdditionalInformationManagement()
        {
            var additionalInfos = _repository.Get();

            return View(additionalInfos);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            var additionalInfo = _repository.Get(id);
            if(additionalInfo != null) _repository.Remove(additionalInfo);

            return RedirectToAction("AdditionalInformationManagement");
        }

        [HttpPost]
        public ActionResult Add(AdditionalInfo additionalInfo)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(additionalInfo);
            }

            return RedirectToAction("AdditionalInformationManagement");
        }

        [HttpPost]
        public ActionResult Update(AdditionalInfo additionalInfo)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(additionalInfo);
            }

            return RedirectToAction("AdditionalInformationManagement");
        }
        
    }
}