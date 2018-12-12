using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly ContactRepository _contactRepository;
        private readonly PrivateInformationRepository _privateInformationRepository;
        private readonly AchievementRepository _achievementRepository;
        private readonly AdditionalInformationRepository _additionalInformationRepository;
        private readonly EducationRepository _educationRepository;
        private readonly EmploymentHistoryRepository _employmentHistoryRepository;
        private readonly ProjectsRepository _projectsRepository;
        private readonly TechnologyRepository _technologyRepository;

        public AdminPanelController(ContactRepository contactRepository, PrivateInformationRepository privateInformationRepository, AchievementRepository achievementRepository, AdditionalInformationRepository additionalInformationRepository, EducationRepository educationRepository, EmploymentHistoryRepository employmentHistoryRepository, ProjectsRepository projectsRepository, TechnologyRepository technologyRepository)
        {
            _contactRepository = contactRepository;
            _privateInformationRepository = privateInformationRepository;
            _achievementRepository = achievementRepository;
            _additionalInformationRepository = additionalInformationRepository;
            _educationRepository = educationRepository;
            _employmentHistoryRepository = employmentHistoryRepository;
            _projectsRepository = projectsRepository;
            _technologyRepository = technologyRepository;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditCv()
        {
            CvViewModel cvViewModel = new CvViewModel()
            {
                Contact = _contactRepository.GetAll().FirstOrDefault(),
                PrivateInformation = _privateInformationRepository.GetAll().FirstOrDefault(),
                Achievements = _achievementRepository.GetAll().ToList(),
                AdditionalInfos = _additionalInformationRepository.GetAll().ToList(),
                Educations = _educationRepository.GetAll().ToList(),
                EmploymentHistories = _employmentHistoryRepository.GetAll().ToList(),
                Projects = _projectsRepository.GetAll().ToList(),
                Technologies = _technologyRepository.GetAll().ToList()
            };


            return View("Cv/EditCv", cvViewModel);
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