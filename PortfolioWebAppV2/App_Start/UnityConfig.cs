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

            container.RegisterType<IRepository<AboutMe, int>, AboutMeRepository>();
            container.RegisterType<IRepository<Achievement, int>, AchievementRepository>();
            container.RegisterType<IRepository<AdditionalInfo, int>, AdditionalInformationRepository>();
            container.RegisterType<IRepository<Contact, int>, ContactRepository>();
            container.RegisterType<IRepository<Education, int>, EducationRepository>();
            container.RegisterType<IRepository<EmploymentHistory, int>, EmploymentHistoryRepository>();
            container.RegisterType<IRepository<PrivateInformation, int>, PrivateInformationRepository>();
            container.RegisterType<IRepository<Technology, int>, TechnologyRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}