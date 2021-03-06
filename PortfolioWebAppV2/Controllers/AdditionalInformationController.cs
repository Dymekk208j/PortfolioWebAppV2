﻿using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PortfolioWebAppV2.Controllers
{
    [Authorize(Roles = "Admin")]
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
            IEnumerable<AdditionalInfo> additionalInfos = _repository.GetAll();

            return View(additionalInfos);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            try
            {
                AdditionalInfo additionalInfo = _repository.Get(id);
                _repository.Remove(additionalInfo);
                return RedirectToAction("AdditionalInformationManagement");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View("ErrorPage");
        }

        [HttpPost]
        public ActionResult Add(AdditionalInfo additionalInfo)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(additionalInfo);
                return JavaScript("reload();");
            }

            return PartialView("_CreateAdditionalInformationPartialView", additionalInfo);

        }

        [HttpPost]
        public ActionResult Update(AdditionalInfo additionalInfo)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(additionalInfo);
                return JavaScript("reload();");
            }

            return PartialView("_UpdateAdditionalInformationPartialView", additionalInfo);
        }

        [HttpPost]
        public ActionResult AddAdditionalInfoToCv(CvViewModel cvModel)
        {
            try
            {
                AdditionalInfo additionalInfo = _repository.Get(cvModel.SelectedAddtinionaInformation);
                additionalInfo.ShowInCv = true;
                if (_repository.Update(additionalInfo)) return RedirectToAction("EditCv", "AdminPanel");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View("ErrorPage");
        }

        [HttpGet]
        public ActionResult RemoveAdditionalInformationFromCv(int id)
        {
            try
            {
                AdditionalInfo additionalInfo = _repository.Get(id);

                additionalInfo.ShowInCv = false;
                if (_repository.Update(additionalInfo)) return RedirectToAction("EditCv", "AdminPanel");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View("ErrorPage");
        }
    }
}