using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class EducationController : Controller
    {
        private IRepository<Education, int> _repository;

        public EducationController(IRepository<Education, int> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public ActionResult EducationManagement()
        {
            var educations = _repository.Get();

            return View(educations);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            var education = _repository.Get(id);
            _repository.Remove(education);

            return RedirectToAction("EducationManagement");
        }

        [HttpPost]
        public ActionResult Add(Education education)
        {
            if (ModelState.IsValid)
            {
              _repository.Add(education);
            }

            return RedirectToAction("EducationManagement");
        }

        [HttpPost]
        public ActionResult Update(Education education)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(education);
            }

            return RedirectToAction("EducationManagement");
        }
    }
}