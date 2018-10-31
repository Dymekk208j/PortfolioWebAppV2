using System.Linq;
using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;

namespace PortfolioWebAppV2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var project = db.Projects.OrderByDescending(a => a.ProjectId).FirstOrDefault();

            return View(project);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var arg = AutoMapper.Mapper.Map<Contact, ContactViewModel>(dbContext.Contacts.FirstOrDefault());

            return View(arg);
        }

        [HttpGet]
        public ActionResult GetSocialMediaBarPartial()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var arg = AutoMapper.Mapper.Map<Contact, ContactViewModel>(dbContext.Contacts.FirstOrDefault());

            return PartialView("_SocialMediaBarPartialView", arg);
        }

        [HttpGet]
        public ActionResult Projects(int? page, bool? commercial)
        {
            int pageNumber = page.GetValueOrDefault();
            bool isCommercial = commercial.GetValueOrDefault();
            
            ApplicationDbContext db = new ApplicationDbContext();
            var projects = db.Projects.Where(a => a.Commercial == isCommercial).OrderByDescending(a => a.ProjectId).Skip(pageNumber * 10).Take(10).ToList();

            ProjectsViewModel projectsViewModel = new ProjectsViewModel()
            {
                Commercial = isCommercial,
                Page = pageNumber,
                Projects = projects
            };

            return View(projectsViewModel);
        }

        [HttpGet]
        public ActionResult Project(int id)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var project = dbContext.Projects.FirstOrDefault(a => a.ProjectId == id);

            return View(project);
        }

        [HttpGet]
        public ActionResult Cv()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            CvViewModel cvViewModel = new CvViewModel()
            {
                Contact = db.Contacts.FirstOrDefault(),
                PrivateInformation = db.PrivateInformations.FirstOrDefault(),
                Achievements = db.Achievements.Where(a => a.ShowInCv).ToList(),
                AdditionalInfos = db.AdditionalInfos.Where(a => a.ShowInCv).ToList(),
                Educations = db.Educations.Where(a => a.ShowInCv).ToList(),
                EmploymentHistories = db.EmploymentHistories.Where(a => a.ShowInCv).ToList(),
                Projects = db.Projects.Where(a => a.ShowInCv).ToList(),
                Technologies = db.Technologies.Where(a => a.ShowInCv).ToList()
            };

            return View(cvViewModel);
        }

        [HttpGet]
        public ActionResult PrintCv()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            CvViewModel cvViewModel = new CvViewModel()
            {
                Contact = db.Contacts.FirstOrDefault(),
                PrivateInformation = db.PrivateInformations.FirstOrDefault(),
                Achievements = db.Achievements.Where(a => a.ShowInCv).ToList(),
                AdditionalInfos = db.AdditionalInfos.Where(a => a.ShowInCv).ToList(),
                Educations = db.Educations.Where(a => a.ShowInCv).ToList(),
                EmploymentHistories = db.EmploymentHistories.Where(a => a.ShowInCv).ToList(),
                Projects = db.Projects.Where(a => a.ShowInCv).ToList(),
                Technologies = db.Technologies.Where(a => a.ShowInCv).ToList()
            };

            return View(cvViewModel);
        }

    }
}