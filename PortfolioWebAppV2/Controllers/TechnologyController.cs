using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class TechnologyController : Controller
    {
        private IRepository<Technology, int> _repository;

        public TechnologyController(IRepository<Technology, int> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public ActionResult TechnologyManagement()
        {
            var technologies = _repository.Get();

            return View("TechnologyManagement", technologies);
        }

        [HttpPost]
        public ActionResult Add(Technology technology)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(technology);
            }

            return RedirectToAction("TechnologyManagement");
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            var technology = _repository.Get(id);
            _repository.Remove(technology);

            return RedirectToAction("TechnologyManagement");
        }

        [HttpPost]
        public ActionResult AddToCv(CvViewModel cvModel, Technology.LevelOfKnowledge levelOfKnowledge)
        {
            Technology technology = _repository.Get(cvModel.SelectedTechnology);

            if (technology != null)
            {
                technology.ShowInCv = true;
                technology.KnowledgeLevel = levelOfKnowledge;

                _repository.Update(technology);
            }

            return RedirectToAction("EditCv", "AdminPanel");
        }

        [HttpGet]
        public ActionResult RemoveFromCv(int id)
        {
            Technology technology = _repository.Get(id);
            if (technology != null)
            {
                technology.ShowInCv = false;
                _repository.Update(technology);
            }

            return RedirectToAction("EditCv", "AdminPanel");
        }

    }
}