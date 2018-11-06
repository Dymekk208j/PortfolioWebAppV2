using System;
using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;

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
        public ActionResult AchievementsManagement()
        {
            var achievements = _repository.GetAll();
            return View(achievements);
        }


        [HttpGet]
        public ActionResult RemoveAchievement(int id)
        {
            try
            {
                var achievement = _repository.Get(id);
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
        public ActionResult AddAchievement(Achievement achievement)
        {
            if (ModelState.IsValid && _repository.Add(achievement))
            {
                return RedirectToAction("AchievementsManagement");
            }
            return View("ErrorPage");
        }

        [HttpPost]
        public ActionResult AddAchievementToCv(CvViewModel cvModel)
        {
            if (_repository.ChangeStatusInCv(cvModel.SelectedAchievement))
            {
                return RedirectToAction("EditCv", "AdminPanel");
            }
            return View("ErrorPage");
        }

        [HttpGet]
        public ActionResult RemoveAchievementFromCv(int id)
        {
            if (_repository.ChangeStatusInCv(id))
            {
                return RedirectToAction("EditCv", "AdminPanel");
            }
            return View("ErrorPage");
        }

    }
}