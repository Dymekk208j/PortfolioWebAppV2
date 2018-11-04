using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class AboutMeController : Controller
    {
        private IRepository<AboutMe, int> _repository;

        public AboutMeController(IRepository<AboutMe, int> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public ActionResult AboutMeManagement()
        {
            AboutMeViewModel aboutMeViewModel = Mapper.Map<AboutMe, AboutMeViewModel>(_repository.GetAll().FirstOrDefault());

            return View("AboutMeManagement", aboutMeViewModel);
        }

        [HttpGet]
        public ActionResult AboutMe()
        {
            var c = _repository.GetAll().FirstOrDefault();
            AboutMeViewModel aboutMeViewModel = Mapper.Map<AboutMe, AboutMeViewModel>(c);

            return View(aboutMeViewModel);
        }

        [HttpPost]
        public ActionResult Update(AboutMeViewModel aboutMeViewModel)
        {
            AboutMe aboutMe = Mapper.Map<AboutMeViewModel, AboutMe>(aboutMeViewModel);
            _repository.Update(aboutMe);

            return RedirectToAction("AboutMeManagement");
        }

    }
}