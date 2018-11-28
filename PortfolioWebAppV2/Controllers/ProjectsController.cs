using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using Microsoft.AspNet.Identity;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectsRepository _repository;

        public ProjectsController(ProjectsRepository repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public ActionResult ProjectManagement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrUpdate(Project project, bool temporary)
        {
            if (ModelState.IsValid)
            {
                project.AuthorId = HttpContext.User.Identity.GetUserId();
                project.TempProject = temporary;

                _repository.AddOrUpdate(project);

                return temporary == false ? RedirectToAction("ProjectsList") : RedirectToAction("TemporaryProjectsList");
            }

            return View("ProjectManagement", project);
        }

        [HttpGet]
        public ActionResult ProjectsList()
        {
            IEnumerable<Project> list = _repository.GetAll().Where(m => m.TempProject == false);

            return View(list);
        }

        [HttpGet]
        public ActionResult TemporaryProjectsList()
        {
            IEnumerable<Project> list = _repository.GetAll().Where(m => m.TempProject);

            return View(list);
        }

      
    }
}