using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PortfolioWebAppV2.Models.DatabaseModels.DatabaseContext;

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

            AutoMapperConfig.Initialize();

            Database.SetInitializer(new DbInitializer());
            UnityConfig.RegisterComponents();
        }
    }
}
