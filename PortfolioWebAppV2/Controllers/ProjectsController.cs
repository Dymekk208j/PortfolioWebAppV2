using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;

namespace PortfolioWebAppV2.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        public ActionResult Index()
        {
            return View(new Project());
        }
    }
}