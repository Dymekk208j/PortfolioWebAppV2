using PortfolioWebAppV2.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Moq;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Models.ViewModels;
using PortfolioWebAppV2.Repository;
using Xunit;

namespace PortfolioWebAppV2Tests1.Controllers
{
    public class AchievementControllerTests
    {
        #region CONFIGURATION
        private readonly List<Achievement> _achievements = new List<Achievement>()
        {
            new Achievement()
            {
                AchievementId = 1,
                Date = DateTime.Parse("2011-01-01"),
                Title = "Pierwszy tytuł",
                Description = "Pierwszy opis",
                ShowInCv = false
            },
            new Achievement()
            {
                AchievementId = 2,
                Date = DateTime.Parse("2013-01-01"),
                Title = "Drugi tytuł",
                Description = "Drugi opis",
                ShowInCv = true
            }
        };
        private readonly Mock<DbSet<Achievement>> _mockSet = new Mock<DbSet<Achievement>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly AchievementController _controller;


        public AchievementControllerTests()
        {
            var achievements = _achievements.AsQueryable();
            _mockSet.As<IQueryable<Achievement>>().Setup(m => m.Provider).Returns(achievements.Provider);
            _mockSet.As<IQueryable<Achievement>>().Setup(m => m.Expression).Returns(achievements.Expression);
            _mockSet.As<IQueryable<Achievement>>().Setup(m => m.ElementType).Returns(achievements.ElementType);
            _mockSet.As<IQueryable<Achievement>>().Setup(m => m.GetEnumerator()).Returns(() => achievements.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<Achievement>())).Callback<Achievement>((s) => _achievements.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<Achievement>())).Callback<Achievement>((s) => _achievements.Remove(s));

            _dbMock.Setup(x => x.SaveChanges()).Returns(1).Verifiable();
            _dbMock.Setup(x => x.Achievements).Returns(_mockSet.Object);


            AchievementRepository repository = new AchievementRepository(_dbMock.Object);
            _controller = new AchievementController(repository);
        }

        #endregion

        [Fact()]
        public void AchievementsManagementTest()
        {
            //Arrange

            //act
            var result = _controller.AchievementsManagement() as ViewResult;

            //Assert
            Assert.NotNull(result);

            var model = result.Model;
            Assert.NotNull(model);

            Assert.Equal(string.Empty, result.ViewName);
        }

        [Fact()]
        public void RedirectToActionAchievementsManagementIfAchievementExist()
        {
            //Arrange

            //Act
            var result = (RedirectToRouteResult)_controller.RemoveAchievement(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("AchievementsManagement", result.RouteValues["action"]);
        }

        [Fact()]
        public void RedirectToErrorPageIfAchievementDoesNotExist()
        {
            //Arrange

            //Act
            var result = _controller.RemoveAchievement(-1) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("ErrorPage", result.ViewName);
        }

        [Fact()]
        public void RedirectToActionToAchievementsManagementIfModelIsValid()
        {
            //Arrange
            var achievement = new Achievement()
            {
                Date = DateTime.Parse("2011-01-01"),
                Title = "Pierwszy tytuł",
                Description = "Pierwszy opis",
                ShowInCv = false
            };

            //Act
            var result = (RedirectToRouteResult)_controller.AddAchievement(achievement);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("AchievementsManagement", result.RouteValues["action"]);
        }

        [Theory()]
        [InlineData("", "Tytuł", "To jest opis")]
        [InlineData("2017-01-01", "", "To jest opis")]
        [InlineData("2017-01-01", "Tytuł", "")]
        public void RedirectToErrorPageWhenModelIsNotValid(string date, string title, string description)
        {
            //Arrange
            DateTime? d;
            if (date == "")
            {
                d = null;
            }
            else d = DateTime.Parse(date);

            var achievement = new Achievement()
            {
                Date = d,
                Title = title,
                Description = description,
                ShowInCv = false
            };

            //Act
            Validate.ValidateModel(_controller, achievement);
            var viewResult = _controller.AddAchievement(achievement) as ViewResult;

            //Assert
            Assert.NotNull(viewResult);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
        }

        [Fact()]
        public void Redirect_To_EditAction_In_AdminPanelController_When_Selected_Achievement_is_exist_while_adding_to_cv()
        {
            //Arrange
            CvViewModel cvViewModel = new CvViewModel()
            {
                SelectedAchievement = 1
            };

            //Act
            var result = (RedirectToRouteResult)_controller.AddAchievementToCv(cvViewModel);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("EditCv", result.RouteValues["action"]);
            Assert.Equal("AdminPanel", result.RouteValues["controller"]);
        }

        [Fact()]
        public void Redirect_to_Error_Page_when_adding_achievement_to_CV_if_it_does_not_exist()
        {
            //Arrange
            CvViewModel cvViewModel = new CvViewModel()
            {
                SelectedAchievement = -1
            };

            //Act
            var viewResult = _controller.AddAchievementToCv(cvViewModel) as ViewResult;
           

            //Assert
            Assert.NotNull(viewResult);
            Assert.Equal("ErrorPage", viewResult.ViewName);
        }

        [Fact()]
        public void Redirect_to_Error_Page_when_removing_achievement_from_CV_if_it_does_not_exist()
        {
            //Arrange

            //Act
            var viewResult = _controller.RemoveAchievementFromCv(-1) as ViewResult;

            //Assert
            Assert.NotNull(viewResult);
            Assert.Equal("ErrorPage", viewResult.ViewName);
        }

        [Fact()]
        public void Redirect_To_EditAction_In_AdminPanelController_When_Selected_Achievement_is_exist_while_removing_from_cv()
        {
            //Arrange

            //Act
            var result = (RedirectToRouteResult)_controller.RemoveAchievementFromCv(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("EditCv", result.RouteValues["action"]);
            Assert.Equal("AdminPanel", result.RouteValues["controller"]);
        }
    }
}