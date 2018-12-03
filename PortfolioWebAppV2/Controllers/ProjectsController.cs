using Microsoft.AspNet.Identity;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PortfolioWebAppV2.Models.ViewModels;

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
            IEnumerable<TechnologyViewModel> technologies =
                Mapper.Map<IEnumerable<Technology>, IEnumerable<TechnologyViewModel>>(
                    _repository.GetAllTechnologies());

            ProjectViewModel project = new ProjectViewModel()
            {
                Technologies = technologies
            };
            
            return View(project);
        }

        [HttpGet]
        public ActionResult EditProject(int projectId)
        {
            Project project = _repository.Get(projectId);
            ProjectViewModel projectViewModel = Mapper.Map<Project, ProjectViewModel>(project);

            return View(projectViewModel);
        }

        [HttpPost]
        public ActionResult Create(ProjectViewModel projectViewModel)
        {
            projectViewModel.Technologies = projectViewModel.Technologies.Where(a => a.IsSelected);
            projectViewModel.Icon = _repository.GetAllIcons().First(m => m.ImageId == projectViewModel.Icon.ImageId);
            projectViewModel.AuthorId = HttpContext.User.Identity.GetUserId();

            Project project = Mapper.Map<ProjectViewModel, Project>(projectViewModel);
            

            if (ModelState.IsValid)
            {

                _repository.Add(project);

                return project.TempProject == false ? RedirectToAction("ProjectsManagement") : RedirectToAction("TemporaryProjectsManagement");
            }

            return View("CreateProject", projectViewModel);
        }

        [HttpPost]
        public ActionResult Update(ProjectViewModel projectViewModel)
        {
            IEnumerable<TechnologyViewModel> tech = projectViewModel.Technologies.Where(a => a.IsSelected);
            projectViewModel.Technologies = tech;
            projectViewModel.AuthorId = HttpContext.User.Identity.GetUserId();

            Project project = Mapper.Map<ProjectViewModel, Project>(projectViewModel);


            if (ModelState.IsValid)
            {

                _repository.Update(project);

                return project.TempProject == false ? RedirectToAction("ProjectsManagement") : RedirectToAction("TemporaryProjectsManagement");
            }

            return View("EditProject", projectViewModel);
        }

        [HttpGet]
        public ActionResult ProjectsManagement()
        {
            IEnumerable<Project> list = _repository.GetAll().Where(m => m.TempProject == false);

            return View(list);
        }

        [HttpGet]
        public ActionResult TemporaryProjectsManagement()
        {
            IEnumerable<Project> list = _repository.GetAll().Where(m => m.TempProject);

            return View(list);
        }

        [HttpGet]
        public ActionResult RemoveProject(int projectId, bool temporary)
        {
            _repository.Remove(projectId);

            return temporary == false ? RedirectToAction("ProjectsManagement") : RedirectToAction("TemporaryProjectsManagement");
        }

        [HttpGet]
        public ActionResult Projects(bool? commercial)
        {
            ViewBag.isCommercial = commercial.GetValueOrDefault();
            IEnumerable<Project> projects = _repository.GetAll().Where(a => a.Commercial == commercial.GetValueOrDefault());

            return View(projects);
        }

        [HttpGet]
        public ActionResult ProjectDetails(int projectId)
        {
            Project project = _repository.Get(projectId);
            return View(project);
        }

        public ActionResult GetTechnologiesPanel()
        {
            IEnumerable<Technology> t = _repository.GetAllTechnologies();

            return PartialView("_TechnologiesPanelPartialView", t);
        }

        public ActionResult GetIconsSelectorPanel()
        {
            IEnumerable<Image> icons = _repository.GetAllIcons();

            return PartialView("_SelectIconPartialView", icons);
        }

        public string GetIconLink(int id)
        {
            Image image = _repository.GetAllIcons().First(i => i.ImageId == id);

            return image.GetLink();
        }
    }
}