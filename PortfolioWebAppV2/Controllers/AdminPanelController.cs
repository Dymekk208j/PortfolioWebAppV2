using System.Linq;
using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;

namespace PortfolioWebAppV2.Controllers
{
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
        public ActionResult UserMgt(UsersManagementViewModel usersManagementViewModel)
        {
            var db = new ApplicationDbContext();
            usersManagementViewModel.Page = usersManagementViewModel.Page.GetValueOrDefault();

            if (usersManagementViewModel.Page < 0)
            {
                usersManagementViewModel.Page = 0;
            }

            var usersList = db.Users.OrderBy(a => a.UserName).ToList();
            
            if (!string.IsNullOrEmpty(usersManagementViewModel.UserNameFilters)) usersList = usersList.Where(d => d.UserName == usersManagementViewModel.UserNameFilters).ToList();
            if (!string.IsNullOrEmpty(usersManagementViewModel.FirstNameFilters)) usersList = usersList.Where(d => d.FirstName == usersManagementViewModel.FirstNameFilters).ToList();
            if (!string.IsNullOrEmpty(usersManagementViewModel.LastNameFilters)) usersList = usersList.Where(d => d.LastName == usersManagementViewModel.LastNameFilters).ToList();
            if (!string.IsNullOrEmpty(usersManagementViewModel.EmailFilters)) usersList = usersList.Where(d => d.Email == usersManagementViewModel.FirstNameFilters).ToList();
            if (usersManagementViewModel.BlockedFilter != null) usersList = usersList.Where(d => d.Blocked == usersManagementViewModel.BlockedFilter).ToList();

            usersList = usersList.Skip(usersManagementViewModel.Page.GetValueOrDefault() * 10).Take(10).ToList();

            usersManagementViewModel.Users = usersList;

            if (Request.IsAjaxRequest())
            {
                return PartialView("Users/_UserListPartialView", usersList);
            }
            return View("Users/UserMgt", usersManagementViewModel);
        }

    }

}