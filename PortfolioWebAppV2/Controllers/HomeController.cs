using System.Linq;
using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactRepository _contactRepository;
        private readonly PrivateInformationRepository _privateInformationRepository;
        private readonly AchievementRepository _achievementRepository;
        private readonly AdditionalInformationRepository _additionalInformationRepository;
        private readonly EducationRepository _educationRepository;
        private readonly EmploymentHistoryRepository _employmentHistoryRepository;
        private readonly ProjectsRepository _projectsRepository;
        private readonly TechnologyRepository _technologyRepository;

        public HomeController(ContactRepository contactRepository, PrivateInformationRepository privateInformationRepository, AchievementRepository achievementRepository, AdditionalInformationRepository additionalInformationRepository, EducationRepository educationRepository, EmploymentHistoryRepository employmentHistoryRepository, ProjectsRepository projectsRepository, TechnologyRepository technologyRepository)
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
            Project project = _projectsRepository.GetAll().OrderByDescending(a => a.ProjectId).FirstOrDefault(p => p.TempProject == false);

            return View(project);
        }

        

        [HttpGet]
        public ActionResult GetSocialMediaBarPartial()
        {
            ContactViewModel arg = AutoMapper.Mapper.Map<Contact, ContactViewModel>(_contactRepository.GetAll().FirstOrDefault());
            return arg == null ? null : PartialView("_SocialMediaBarPartialView", arg);
        }
      
        [HttpGet]
        public ActionResult Cv()
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

            return View(cvViewModel);
        }

        [HttpGet]
        public ActionResult PrintCv()
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

            return View(cvViewModel);
        }

    }
}