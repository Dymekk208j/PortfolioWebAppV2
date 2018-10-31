using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class AchievementController : Controller
    {
        private IRepository<Achievement, int> _repository;

        public AchievementController(IRepository<Achievement, int> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public ActionResult AchievementsManagement()
        {
            var achievements = _repository.Get();
            return View(achievements);
        }
        

        [HttpGet]
        public ActionResult RemoveAchievement(int id)
        {
            var achievement = _repository.Get(id);
            _repository.Remove(achievement);

            return RedirectToAction("AchievementsManagement");
        }

        [HttpPost]
        public ActionResult AddAchievement(Achievement achievement)
        {
            if (ModelState.IsValid)
            {
              _repository.Add(achievement);
            }

            return RedirectToAction("AchievementsManagement");
        }
    }
}