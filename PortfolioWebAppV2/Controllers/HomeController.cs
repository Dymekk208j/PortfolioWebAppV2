using System.Linq;
using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;

namespace PortfolioWebAppV2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutMe()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var aboutMe = dbContext.AboutMe.FirstOrDefault();
            var arg = AutoMapper.Mapper.Map<AboutMe, AboutMeViewModel>(aboutMe);

            return View(arg);
        }
    }
}