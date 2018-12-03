using System.Web.Mvc;
using PortfolioWebAppV2.Controllers;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using Unity;
using Unity.Injection;
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
            container.RegisterType<IRepository<Project, int>, ProjectsRepository>();
            container.RegisterType<IRepository<Image, int>, ImageRepository>();


            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}