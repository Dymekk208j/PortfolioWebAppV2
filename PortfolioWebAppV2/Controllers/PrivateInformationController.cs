using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Controllers
{
    public class PrivateInformationController : Controller
    {
        private IRepository<PrivateInformation, int> _repository;

        public PrivateInformationController(IRepository<PrivateInformation, int> repo)
        {
            _repository = repo;
        }

        [HttpPost]
        public void Update(PrivateInformation privateInformation)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(privateInformation);
            }
        }
    }
}