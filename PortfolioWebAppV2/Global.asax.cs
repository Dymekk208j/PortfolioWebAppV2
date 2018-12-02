using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.DatabaseModels.DatabaseContext;
using PortfolioWebAppV2.Models.ViewModels;

namespace PortfolioWebAppV2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(config =>
            {
                config.CreateMap<TechnologyViewModel, Technology>().ReverseMap();
                config.CreateMap<ProjectViewModel, Project>().ReverseMap();
                config.CreateMap<AboutMeViewModel, AboutMe>().ReverseMap();

            });

            Database.SetInitializer(new DbInitializer());
            UnityConfig.RegisterComponents();
        }
    }
}
