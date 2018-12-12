using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;

namespace PortfolioWebAppV2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditCv()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            CvViewModel cvViewModel = new CvViewModel()
            {
                Contact = db.Contacts.FirstOrDefault(),
                PrivateInformation = db.PrivateInformations.FirstOrDefault(),
                Achievements = db.Achievements.ToList(),
                AdditionalInfos = db.AdditionalInfos.ToList(),
                Educations = db.Educations.ToList(),
                EmploymentHistories = db.EmploymentHistories.ToList(),
                Projects = db.Projects.ToList(),
                Technologies = db.Technologies.ToList()
            };

            return View("Cv/EditCv", cvViewModel);
        }
        
        [HttpPost]
        public ActionResult AddProjectToCv(CvViewModel cvModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = new Project() { ProjectId = cvModel.SelectedProject, ShowInCv = true };

            db.Projects.Attach(project);
            db.Entry(project).Property(x => x.ShowInCv).IsModified = true;
            db.SaveChanges();

            return RedirectToAction("EditCv");
        }

        [HttpGet]
        public ActionResult RemoveProjectFromCv(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Project project = new Project() { ProjectId = id, ShowInCv = false };

            db.Projects.Attach(project);
            db.Entry(project).Property(x => x.ShowInCv).IsModified = true;
            db.SaveChanges();

            return RedirectToAction("EditCv");
        }

        [HttpGet]
        public ActionResult UserMgt()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<ApplicationUser> usersList = db.Users.Where(u => u.Blocked == false).OrderBy(a => a.UserName).ToList();
            
            return View("Users/UserMgt", usersList);
        }

        [HttpGet]
        public ActionResult BlockedUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<ApplicationUser> usersList = db.Users.Where(u => u.Blocked).OrderBy(a => a.UserName).ToList();

            return View("Users/BlockedUsers", usersList);
        }

    }

}