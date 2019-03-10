using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

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
        [Authorize(Roles = "Admin")]
        public ActionResult CreateProject()
        {
            IEnumerable<TechnologyViewModel> technologies =
                Mapper.Map<IEnumerable<Technology>, IEnumerable<TechnologyViewModel>>(
                    _repository.GetAllTechnologies());

            ProjectViewModel project = new ProjectViewModel()
            {
                Technologies = technologies,
                Icon = new Image()
                {
                    ImageId = -1
                }
            };

            return View(project);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditProject(int projectId)
        {
            Project project = _repository.Get(projectId);
            ProjectViewModel projectViewModel = Mapper.Map<Project, ProjectViewModel>(project);

            return View(projectViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ProjectViewModel projectViewModel)
        {
            projectViewModel.Technologies = projectViewModel.Technologies.Where(a => a.IsSelected);
            if (projectViewModel.TempProject == false)
            {
                if (projectViewModel.Icon.ImageId < 1)
                {
                    ModelState.AddModelError("IconError", @"Musisz wybrać ikonę");
                }
                else
                    projectViewModel.Icon =
                        _repository.GetAllIcons().First(m => m.ImageId == projectViewModel.Icon.ImageId);
            }
            else projectViewModel.Icon = projectViewModel.Icon.ImageId < 1 ?
               null : _repository.GetAllIcons().First(m => m.ImageId == projectViewModel.Icon.ImageId);

            projectViewModel.AuthorId = HttpContext.User.Identity.GetUserId();

            Project project = Mapper.Map<ProjectViewModel, Project>(projectViewModel);


            if (ModelState.IsValid)
            {

                _repository.Add(project);

                // return project.TempProject == false ? RedirectToAction("ProjectsManagement") : RedirectToAction("TemporaryProjectsManagement");
                return RedirectToAction("ScreenshotsManagement", "Image", new { projectId = project.ProjectId });
            }

            return View("CreateProject", projectViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(ProjectViewModel projectViewModel)
        {
            IEnumerable<TechnologyViewModel> tech = projectViewModel.Technologies.Where(a => a.IsSelected);
            projectViewModel.Technologies = tech;
            if (projectViewModel.Icon.ImageId == 0)
            {
                ModelState.AddModelError("IconError", @"Musisz wybrać ikonę");
            }
            else projectViewModel.Icon = _repository.GetAllIcons().First(m => m.ImageId == projectViewModel.Icon.ImageId);
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
        [Authorize(Roles = "Admin")]
        public ActionResult ProjectsManagement()
        {
            IEnumerable<Project> list = _repository.GetAll().Where(m => m.TempProject == false);

            return View(list);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult TemporaryProjectsManagement()
        {
            IEnumerable<Project> list = _repository.GetAll().Where(m => m.TempProject);

            return View(list);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveProject(int projectId, bool temporary)
        {
            _repository.Remove(projectId);

            return temporary == false ? RedirectToAction("ProjectsManagement") : RedirectToAction("TemporaryProjectsManagement");
        }

        [HttpGet]
        public ActionResult Projects(bool? commercial)
        {
            ViewBag.isCommercial = commercial.GetValueOrDefault();
            IEnumerable<Project> projects = _repository.GetAll().Where(a => a.Commercial == commercial.GetValueOrDefault()).ToList();

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

        [HttpPost]
        public ActionResult AddProjectToCv(CvViewModel cvModel)
        {
            _repository.AddToPortfolio(cvModel.SelectedProject);

            return RedirectToAction("EditCv", "AdminPanel");
        }

        [HttpGet]
        public ActionResult RemoveProjectFromCv(int id)
        {
            _repository.RemoveFromPortfolio(id);

            return RedirectToAction("EditCv", "AdminPanel");
        }

        [HttpGet]
        public void PreviewXmlFile(int projectId)
        {
            var project = _repository.Get(projectId);
            var json = JsonConvert.SerializeObject(project);

            XmlDocument xml = JsonConvert.DeserializeXmlNode(json, "ProjectViewModel");

            Response.Clear();
            Response.Write(xml.OuterXml);
            Response.ContentType = "text/xml";
            Response.End();
        }

        [HttpGet]
        public FileResult ExportXmlFile(int projectId)
        {
            var project = _repository.Get(projectId);
            project.Images = null;

            var json = JsonConvert.SerializeObject(project);

            XmlDocument xml = JsonConvert.DeserializeXmlNode(json, "ProjectViewModel");

            return File(Encoding.UTF8.GetBytes(xml.OuterXml), "application/xml", project.Title + ".xml");
        }


        public ActionResult ImportXmlFile(HttpPostedFileBase file, ProjectViewModel project)
        {
            if (file.ContentLength > 0 && file.ContentType == "text/xml")
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(file.InputStream);

                var json = JsonConvert.SerializeXmlNode(xml, Formatting.None, true);

                Project proj = JsonConvert.DeserializeObject<Project>(json);
                proj.ProjectId = project.ProjectId;

                ProjectViewModel projectViewModel = Mapper.Map<Project, ProjectViewModel>(proj);

                ModelState.Clear();
                return View("EditProject", projectViewModel);
            }

            ModelState.AddModelError("CustomError", @"Niepoprawny format przesłanego pliku.");
            return View("EditProject", project);
        }
    }
}