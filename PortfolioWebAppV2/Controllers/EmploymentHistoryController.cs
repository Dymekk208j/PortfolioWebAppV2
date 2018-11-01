using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class EmploymentHistoryController : Controller
    {
        private IRepository<EmploymentHistory, int> _repository;

        public EmploymentHistoryController(IRepository<EmploymentHistory, int> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public ActionResult EmploymentHistoryManagement()
        {
            var employmentHistory = _repository.Get();

            return View(employmentHistory);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            var employmentHistory = _repository.Get(id);
            _repository.Remove(employmentHistory);

            return RedirectToAction("EmploymentHistoryManagement");
        }

        [HttpPost]
        public ActionResult Add(EmploymentHistory employmentHistory)
        {
            if (ModelState.IsValid)
            {
             _repository.Add(employmentHistory);
            }

            return RedirectToAction("EmploymentHistoryManagement");
        }

        [HttpPost]
        public ActionResult Update(EmploymentHistory employmentHistory)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(employmentHistory);
            }

            return RedirectToAction("EmploymentHistoryManagement");
        }

        [HttpPost]
        public ActionResult AddEmploymentHistoryToCv(CvViewModel cvModel)
        {
            EmploymentHistory employmentHistory = _repository.Get(cvModel.SelectedEmploymentHistory);
            if (employmentHistory != null)
            {
                employmentHistory.ShowInCv = true;
                _repository.Update(employmentHistory);
            }

            return RedirectToAction("EditCv", "AdminPanel");
        }

        [HttpGet]
        public ActionResult RemoveEmploymentHistoryFromCv(int id)
        {
            EmploymentHistory employmentHistory = _repository.Get(id);
            if (employmentHistory != null)
            {
                employmentHistory.ShowInCv = false;
                _repository.Update(employmentHistory);
            }

            return RedirectToAction("EditCv", "AdminPanel");
        }
    }
}