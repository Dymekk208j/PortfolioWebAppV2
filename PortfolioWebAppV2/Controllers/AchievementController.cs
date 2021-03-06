﻿using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PortfolioWebAppV2.Controllers
{
    public class AchievementController : Controller
    {
        private AchievementRepository _repository;

        public AchievementController(AchievementRepository repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AchievementsManagement()
        {
            IEnumerable<Achievement> achievements = _repository.GetAll();
            return View(achievements);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Remove(int id)
        {
            try
            {
                Achievement achievement = _repository.Get(id);
                _repository.Remove(achievement);
                return RedirectToAction("AchievementsManagement");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View("ErrorPage");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(achievement);
                return JavaScript("reload();");
            }

            return PartialView("_CreateAchievementPartialView", achievement);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(achievement);
                return JavaScript("reload();");
            }

            return PartialView("_UpdateAchievementPartialView", achievement);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddAchievementToCv(CvViewModel cvModel)
        {
            if (_repository.ChangeStatusInCv(cvModel.SelectedAchievement))
            {
                return RedirectToAction("EditCv", "AdminPanel");
            }
            return RedirectToAction("ErrorPage");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveAchievementFromCv(int id)
        {
            if (_repository.ChangeStatusInCv(id))
            {
                return RedirectToAction("EditCv", "AdminPanel");
            }
            return RedirectToAction("ErrorPage");
        }

        public ActionResult ErrorPage()
        {
            return View();
        }

    }
}