using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
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
        public ActionResult EditContact()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ContactViewModel contactViewModel = Mapper.Map<Contact, ContactViewModel>(db.Contacts.FirstOrDefault());

            return View("Contact/EditContact", contactViewModel);
        }

        [HttpPost]
        public ActionResult UpdateContact(ContactViewModel contactViewModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Contact contact = db.Contacts.FirstOrDefault();

            if (contact != null)
            {
                var c = Mapper.Map<ContactViewModel, Contact>(contactViewModel);
                contact.PhoneNumber = c.PhoneNumber;
                contact.Email1 = c.Email1;
                contact.Email2 = c.Email2;
                contact.FacebookLink = c.FacebookLink;
                contact.GitHubLink = c.GitHubLink;
                contact.LinkedInLink = c.LinkedInLink;

                db.SaveChanges();
            }

            return RedirectToAction("EditContact");
        }


        

        [HttpGet]
        public ActionResult EditTechnologies()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var technologies = db.Technologies.ToList();

            return View("Technologies/EditTechnologies", technologies);
        }

        [HttpPost]
        public ActionResult CreateTechnology(Technology technology)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                db.Technologies.Add(technology);
                db.SaveChanges();
            }

            return RedirectToAction("EditTechnologies");
        }

        [HttpGet]
        public ActionResult RemoveTechnology(int id)
        {
            var db = new ApplicationDbContext();

            var technology = new Technology() { TechnologyId = id };
            db.Technologies.Attach(technology);
            db.Technologies.Remove(technology);
            db.SaveChanges();

            return RedirectToAction("EditTechnologies");
        }

        

        [HttpGet]
        public ActionResult EditEducation()
        {
            var db = new ApplicationDbContext();
            var educations = db.Educations.ToList();

            return View("Educations/EditEducation", educations);
        }

        [HttpGet]
        public ActionResult RemoveEducation(int id)
        {
            var db = new ApplicationDbContext();

            var education = new Education() { EducationId = id };
            db.Educations.Attach(education);
            db.Educations.Remove(education);
            db.SaveChanges();

            return RedirectToAction("EditEducation");
        }

        [HttpPost]
        public ActionResult CreateOrUpdateEducation(Education education)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                db.Educations.AddOrUpdate(education);
                db.SaveChanges();
            }

            return RedirectToAction("EditEducation");
        }

        [HttpGet]
        public ActionResult EditAdditionalInfo()
        {
            var db = new ApplicationDbContext();
            var additionalInfos = db.AdditionalInfos.ToList();

            return View("AdditionalInfos/EditAdditionalInfo", additionalInfos);
        }

        [HttpGet]
        public ActionResult RemoveAdditionalInfo(int id)
        {
            var db = new ApplicationDbContext();

            var additionalInfo = new AdditionalInfo() { AdditionalInfoId = id };
            db.AdditionalInfos.Attach(additionalInfo);
            db.AdditionalInfos.Remove(additionalInfo);
            db.SaveChanges();

            return RedirectToAction("EditAdditionalInfo");
        }

        [HttpPost]
        public ActionResult CreateOrUpdateAdditionalInfo(AdditionalInfo additionalInfo)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                db.AdditionalInfos.AddOrUpdate(additionalInfo);
                db.SaveChanges();
            }

            return RedirectToAction("EditAdditionalInfo");
        }

        [HttpGet]
        public ActionResult EditEmploymentHistory()
        {
            var db = new ApplicationDbContext();
            var employmentHistory = db.EmploymentHistories.ToList();

            return View("EmploymentHistories/EditEmploymentHistory", employmentHistory);
        }

        [HttpGet]
        public ActionResult RemoveEmploymentHistory(int id)
        {
            var db = new ApplicationDbContext();

            var employmentHistory = new EmploymentHistory() { EmploymentHistoryId = id };
            db.EmploymentHistories.Attach(employmentHistory);
            db.EmploymentHistories.Remove(employmentHistory);
            db.SaveChanges();

            return RedirectToAction("EditEmploymentHistory");
        }

        [HttpPost]
        public ActionResult CreateOrUpdateEmploymentHistory(EmploymentHistory employmentHistory)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                db.EmploymentHistories.AddOrUpdate(employmentHistory);
                db.SaveChanges();
            }

            return RedirectToAction("EditEmploymentHistory");
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
        public ActionResult CreateOrUpdatePrivateInformation(PrivateInformation privateInformation)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                db.PrivateInformations.AddOrUpdate(privateInformation);
                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
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

        [HttpPost]
        public ActionResult AddToCvVeryWellKnowTechnology(CvViewModel cvModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Technology technology = db.Technologies.SingleOrDefault(x => x.TechnologyId == cvModel.SelectedTechnology);
            if (technology != null)
            {
                technology.ShowInCv = true;
                technology.KnowledgeLevel = Technology.LevelOfKnowledge.VeryWell;

                db.Technologies.Attach(technology);

                db.Entry(technology).Property(x => x.ShowInCv).IsModified = true;
                db.Entry(technology).Property(x => x.KnowledgeLevel).IsModified = true;

                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpPost]
        public ActionResult AddToCvWellKnowTechnology(CvViewModel cvModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Technology technology = db.Technologies.SingleOrDefault(x => x.TechnologyId == cvModel.SelectedTechnology);
            if (technology != null)
            {
                technology.ShowInCv = true;
                technology.KnowledgeLevel = Technology.LevelOfKnowledge.Well;

                db.Technologies.Attach(technology);

                db.Entry(technology).Property(x => x.ShowInCv).IsModified = true;
                db.Entry(technology).Property(x => x.KnowledgeLevel).IsModified = true;

                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpPost]
        public ActionResult AddToCvKnowTechnology(CvViewModel cvModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Technology technology = db.Technologies.SingleOrDefault(x => x.TechnologyId == cvModel.SelectedTechnology);
            if (technology != null)
            {
                technology.ShowInCv = true;
                technology.KnowledgeLevel = Technology.LevelOfKnowledge.Ok;

                db.Technologies.Attach(technology);

                db.Entry(technology).Property(x => x.ShowInCv).IsModified = true;
                db.Entry(technology).Property(x => x.KnowledgeLevel).IsModified = true;

                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpGet]
        public ActionResult RemoveTechnologyFromCv(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Technology technology = db.Technologies.SingleOrDefault(x => x.TechnologyId == id);
            if (technology != null)
            {
                technology.ShowInCv = false;
                db.Technologies.Attach(technology);
                db.Entry(technology).Property(x => x.ShowInCv).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("EditCv");

        }

        [HttpPost]
        public ActionResult AddAchievementToCv(CvViewModel cvModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Achievement achievement = db.Achievements.SingleOrDefault(x => x.AchievementId == cvModel.SelectedAchievement);
            if (achievement != null)
            {
                achievement.ShowInCv = true;
                db.Achievements.Attach(achievement);
                db.Entry(achievement).Property(x => x.ShowInCv).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpGet]
        public ActionResult RemoveAchievementFromCv(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Achievement achievement = db.Achievements.SingleOrDefault(x => x.AchievementId == id);
            if (achievement != null)
            {
                achievement.ShowInCv = false;
                db.Achievements.Attach(achievement);
                db.Entry(achievement).Property(x => x.ShowInCv).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpGet]
        public ActionResult RemoveAdditionalInformationFromCv(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AdditionalInfo additionalInfo = db.AdditionalInfos.SingleOrDefault(x => x.AdditionalInfoId == id);
            if (additionalInfo != null)
            {
                additionalInfo.ShowInCv = false;
                db.AdditionalInfos.Attach(additionalInfo);
                db.Entry(additionalInfo).Property(x => x.ShowInCv).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpPost]
        public ActionResult AddAdditionalInfoToCv(CvViewModel cvModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AdditionalInfo additionalInfo = db.AdditionalInfos.SingleOrDefault(x => x.AdditionalInfoId == cvModel.SelectedAddtinionaInformation);
            if (additionalInfo != null)
            {
                additionalInfo.ShowInCv = true;
                db.AdditionalInfos.Attach(additionalInfo);
                db.Entry(additionalInfo).Property(x => x.ShowInCv).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpPost]
        public ActionResult AddEmploymentHistroyToCv(CvViewModel cvModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            EmploymentHistory employmentHistory = db.EmploymentHistories.SingleOrDefault(x => x.EmploymentHistoryId == cvModel.SelectedEmploymentHistory);
            if (employmentHistory != null)
            {
                employmentHistory.ShowInCv = true;
                db.EmploymentHistories.Attach(employmentHistory);
                db.Entry(employmentHistory).Property(x => x.ShowInCv).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpGet]
        public ActionResult RemoveEmploymentHistroyFromCv(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            EmploymentHistory employmentHistory = db.EmploymentHistories.SingleOrDefault(x => x.EmploymentHistoryId == id);
            if (employmentHistory != null)
            {
                employmentHistory.ShowInCv = false;
                db.EmploymentHistories.Attach(employmentHistory);
                db.Entry(employmentHistory).Property(x => x.ShowInCv).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpPost]
        public ActionResult AddEducationToCv(CvViewModel cvModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Education education = db.Educations.SingleOrDefault(x => x.EducationId == cvModel.SelectedEducation);
            if (education != null)
            {
                education.ShowInCv = true;
                db.Educations.Attach(education);
                db.Entry(education).Property(x => x.ShowInCv).IsModified = true;
                db.SaveChanges();
            }

            return RedirectToAction("EditCv");
        }

        [HttpGet]
        public ActionResult RemoveEducationFromCv(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Education education = db.Educations.SingleOrDefault(x => x.EducationId == id);
            if (education != null)
            {
                education.ShowInCv = false;
                db.Educations.Attach(education);
                db.Entry(education).Property(x => x.ShowInCv).IsModified = true;
                db.SaveChanges();
            }

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

        //[HttpGet]
        //public ActionResult ProjectsList()
        //{
        //    return View();
        //}
    }

}