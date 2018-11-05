using AutoMapper;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;

namespace PortfolioWebAppV2
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<AboutMeViewModel, AboutMe>().ReverseMap();
                config.CreateMap<ContactViewModel, Contact>().ReverseMap();
                config.CreateMap<UpdateViewModel, ApplicationUser>().ReverseMap();

            });
        }
    }
}