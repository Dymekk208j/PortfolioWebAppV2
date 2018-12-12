using System.Collections.Generic;
using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    [Authorize(Roles = "Admin")]
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
            IEnumerable<Education> educations = _repository.GetAll();

            return View(educations);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            Education education = _repository.Get(id);
            _repository.Remove(education);

            return RedirectToAction("EducationManagement");
        }

        [HttpPost]
        public ActionResult Add(Education education)
        {
            if (ModelState.IsValid)
            {
              _repository.Add(education);
              return JavaScript("reload();");
            }

            return PartialView("_CreateEducationPartialView", education);
        }

        [HttpPost]
        public ActionResult Update(Education education)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(education);

                return JavaScript("reload();");
            }

            return PartialView("_EditEducationPartialView", education);
        }

        [HttpPost]
        public ActionResult AddEducationToCv(CvViewModel cvModel)
        {
            Education education = _repository.Get(cvModel.SelectedEducation);
            if (education != null)
            {
                education.ShowInCv = true;
                _repository.Update(education);
            }

            return RedirectToAction("EditCv", "AdminPanel");

        }

        [HttpGet]
        public ActionResult RemoveEducationFromCv(int id)
        {
            Education education = _repository.Get(id);
            if (education != null)
            {
                education.ShowInCv = false;
                _repository.Update(education);
            }

            return RedirectToAction("EditCv", "AdminPanel");
        }

    }
}