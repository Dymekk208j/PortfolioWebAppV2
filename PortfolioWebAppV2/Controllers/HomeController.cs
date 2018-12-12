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
            Project project = db.Projects.OrderByDescending(a => a.ProjectId).FirstOrDefault();

            return View(project);
        }

        

        [HttpGet]
        public ActionResult GetSocialMediaBarPartial()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            ContactViewModel arg = AutoMapper.Mapper.Map<Contact, ContactViewModel>(dbContext.Contacts.FirstOrDefault());
            if (arg == null) return null;
            return PartialView("_SocialMediaBarPartialView", arg);
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