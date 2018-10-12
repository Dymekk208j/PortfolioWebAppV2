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
            return View();
        }

        [HttpGet]
        public ActionResult AboutMe()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var arg = AutoMapper.Mapper.Map<AboutMe, AboutMeViewModel>(dbContext.AboutMe.FirstOrDefault());

            return View(arg);
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
    }
}