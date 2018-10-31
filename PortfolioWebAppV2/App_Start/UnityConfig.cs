using System.Web.Mvc;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using Unity;
using Unity.Mvc5;

namespace PortfolioWebAppV2
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IRepository<Achievement, int>, AchievementRepository>();
            container.RegisterType<IRepository<AboutMe, int>, AboutMeRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}