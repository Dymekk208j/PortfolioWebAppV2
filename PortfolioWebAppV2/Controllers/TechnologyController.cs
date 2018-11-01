using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
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
    }
}