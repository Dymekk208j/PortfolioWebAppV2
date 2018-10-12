using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortfolioWebAppV2.Startup))]
namespace PortfolioWebAppV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
