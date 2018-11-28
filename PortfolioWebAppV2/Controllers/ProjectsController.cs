using Microsoft.AspNet.Identity;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
        public ActionResult CreateProject()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditProject(int projectId)
        {
            var project = _repository.Get(projectId);

            return View(project);
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.AuthorId = HttpContext.User.Identity.GetUserId();

                _repository.AddOrUpdate(project);

                return project.TempProject == false ? RedirectToAction("ProjectsList") : RedirectToAction("TemporaryProjectsList");
            }

            return View("CreateProject", project);
        }

        [HttpPost]
        public ActionResult Update(Project project)
        {
            if (ModelState.IsValid)
            {
                project.AuthorId = HttpContext.User.Identity.GetUserId();

                _repository.AddOrUpdate(project);

                return project.TempProject == false ? RedirectToAction("ProjectsList") : RedirectToAction("TemporaryProjectsList");
            }

            return View("EditProject", project);
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

        public ActionResult RemoveProject(int projectId, bool temporary)
        {
            _repository.Remove(projectId);

            return temporary == false ? RedirectToAction("ProjectsList") : RedirectToAction("TemporaryProjectsList");
        }
    }
}