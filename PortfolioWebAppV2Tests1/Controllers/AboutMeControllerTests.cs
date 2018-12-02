using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Moq;
using PortfolioWebAppV2.Controllers;
using Xunit;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2Tests1.Controllers
{
    public class AboutMeControllerTests : IDisposable
    {
        #region CONFIGURATION
        private readonly List<AboutMe> _aboutMe = new List<AboutMe>()
        {
            new AboutMe()
            {
                AboutMeId = 1,
                ImageLink = "http://via.placeholder.com/250x300",
                Text = "Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod",
                Title = "Tytuł: o mnie:"
            }
        };

        private readonly Mock<DbSet<AboutMe>> _mockSet = new Mock<DbSet<AboutMe>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly AboutMeController _controller;

        public AboutMeControllerTests()
        {
            IQueryable<AboutMe> aboutMes = _aboutMe.AsQueryable();
            _mockSet.As<IQueryable<AboutMe>>().Setup(m => m.Provider).Returns(aboutMes.Provider);
            _mockSet.As<IQueryable<AboutMe>>().Setup(m => m.Expression).Returns(aboutMes.Expression);
            _mockSet.As<IQueryable<AboutMe>>().Setup(m => m.ElementType).Returns(aboutMes.ElementType);
            _mockSet.As<IQueryable<AboutMe>>().Setup(m => m.GetEnumerator()).Returns(() => aboutMes.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<AboutMe>())).Callback<AboutMe>((s) => _aboutMe.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<AboutMe>())).Callback<AboutMe>((s) => _aboutMe.Remove(s));

            _dbMock.Setup(x => x.SaveChanges()).Returns(1).Verifiable();

            _dbMock.Setup(x => x.AboutMe).Returns(_mockSet.Object);


            IRepository<AboutMe, int> repository = new AboutMeRepository(_dbMock.Object);
            _controller = new AboutMeController(repository);

            Mapper.Initialize(config =>
            {
                config.CreateMap<AboutMeViewModel, AboutMe>().ReverseMap();
                config.CreateMap<ContactViewModel, Contact>().ReverseMap();
                config.CreateMap<UpdateViewModel, ApplicationUser>().ReverseMap();

            });

        }

       
        #endregion



        [Fact()]
        public void RedirectToAboutMeManagementPageIfViewModelIsValid()
        {
            //Arrange
            AboutMeViewModel updateAboutMe = new AboutMeViewModel()
            {
                ImageLink = "imageLink",
                Text = "To jest minimalnie 10 znakow",
                Title = "title"
            };
            Validate.ValidateModel(_controller, updateAboutMe);


            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)_controller.Update(updateAboutMe);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("AboutMeManagement", result.RouteValues["action"]);
        }


        [Theory()]
        [InlineData("", "To jest minimalnie 10 znakow", "title")]
        [InlineData("12345", "", "title")]
        [InlineData("12345", "To jest minimalnie 10 znakow", "")]

        public void ShouldFailValidationWhenFieldIsEmpty(string imageLink, string text, string title)
        {
            //Arrange
            AboutMeViewModel updateAboutMe = new AboutMeViewModel()
            {
                ImageLink = imageLink,
                Text = text,
                Title = title
            };

            Validate.ValidateModel(_controller, updateAboutMe);


            //Act
            ViewResult viewResult = _controller.Update(updateAboutMe) as ViewResult;

            //Assert
            Assert.NotNull(viewResult);
            Assert.False(viewResult.ViewData.ModelState.IsValid);

        }


        public void Dispose()
        {
            Mapper.Reset();

        }

        [Fact()]
        public void AboutMeTest()
        {
            //Arrange

            //act
            ViewResult result = _controller.AboutMe() as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.ViewName);

        }

        [Fact()]
        public void AboutMeManagementTest()
        {
            //Arrange

            //act
            ViewResult result = _controller.AboutMeManagement() as ViewResult;

            //Assert
            Assert.NotNull(result);

            object model = result.Model;
            Assert.NotNull(model);

            Assert.Equal("AboutMeManagement", result.ViewName);

        }
    }
}