using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

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
            var aboutMes = _aboutMe.AsQueryable();
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

        public static void ValidateModel(Controller controller, object viewModel)
        {
            controller.ModelState.Clear();

            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResult = new List<ValidationResult>();

            Validator.TryValidateObject(viewModel, validationContext, validationResult, true);
            foreach (var result in validationResult)
            {
                foreach (var name in result.MemberNames)
                {
                    controller.ModelState.AddModelError(name, result.ErrorMessage);
                }
            }
        }

        [Fact()]
        public void RedirectToAboutMeManagementPageIfViewModelIsValid() 
        {
            //Arrange
            var updateAboutMe = new AboutMeViewModel()
            {
                ImageLink = "imageLink",
                Text = "To jest minimalnie 10 znakow",
                Title = "title"
            };
            ValidateModel(_controller, updateAboutMe);


            //Act
            var result = (RedirectToRouteResult)_controller.Update(updateAboutMe);

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
            var updateAboutMe = new AboutMeViewModel()
            {
                ImageLink = imageLink,
                Text = text,
                Title = title
            };

            ValidateModel(_controller, updateAboutMe);


            //Act
            var viewResult = _controller.Update(updateAboutMe) as ViewResult;

            //Assert
            Assert.NotNull(viewResult);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            
        }

        public void Dispose()
        {
            Mapper.Reset();
            
        }
    }
}