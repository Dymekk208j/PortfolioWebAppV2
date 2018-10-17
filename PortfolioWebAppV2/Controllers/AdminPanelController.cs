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

            return View(contactViewModel);
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
        public ActionResult EditAboutMe()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AboutMeViewModel aboutMeViewModel = Mapper.Map<AboutMe, AboutMeViewModel>(db.AboutMe.FirstOrDefault());


            return View(aboutMeViewModel);
        }

        [HttpPost]
        public ActionResult UpdateAboutMe(AboutMeViewModel aboutMeViewModel)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var abm = db.AboutMe.FirstOrDefault();

            if (abm != null)
            {
                AboutMe aboutMe = Mapper.Map<AboutMeViewModel, AboutMe>(aboutMeViewModel);
                aboutMe.AboutMeId = abm.AboutMeId;
                db.Entry(aboutMe).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("EditAboutMe");
        }

        [HttpGet]
        public ActionResult EditTechnologies()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var technologies = db.Technologies.ToList();

            return View(technologies);
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
        public ActionResult EditAchivments()
        {
            var db = new ApplicationDbContext();
            var achivments = db.Achivments.ToList();

            return View(achivments);
        }

        [HttpGet]
        public ActionResult RemoveAchivment(int id)
        {
            var db = new ApplicationDbContext();

            var achivment = new Achivment() { AchivmentId = id };
            db.Achivments.Attach(achivment);
            db.Achivments.Remove(achivment);
            db.SaveChanges();

            return RedirectToAction("EditAchivments");
        }

        [HttpPost]
        public ActionResult CreateAchivment(Achivment achivment)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                db.Achivments.Add(achivment);
                db.SaveChanges();
            }

            return RedirectToAction("EditAchivments");
        }

        [HttpGet]
        public ActionResult EditEducation()
        {
            var db = new ApplicationDbContext();
            var educations = db.Educations.ToList();

            return View(educations);
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

            return View(additionalInfos);
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

            return View(employmentHistory);
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

    }
}