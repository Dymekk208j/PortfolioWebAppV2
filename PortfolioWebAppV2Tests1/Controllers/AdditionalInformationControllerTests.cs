using Moq;
using PortfolioWebAppV2.Controllers;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace PortfolioWebAppV2Tests1.Controllers
{
    public class AdditionalInformationControllerTests
    {
        #region CONFIGURATION
        private readonly List<AdditionalInfo> _additionalInfos = new List<AdditionalInfo>
        {
            new AdditionalInfo() { AdditionalInfoId = 1, Type = AdditionalInfo.TypeOfAddtionalInfo.ForeignLanguages, Title = "Język angielski - Poziom podstawowy", ShowInCv = true},
            new AdditionalInfo() { AdditionalInfoId = 2, Type = AdditionalInfo.TypeOfAddtionalInfo.AdditionalSkills, Title = "Prawo jazdy B. (Dodatkowa um)", ShowInCv = true },
            new AdditionalInfo() { AdditionalInfoId = 3, Type = AdditionalInfo.TypeOfAddtionalInfo.Interests, Title = "Gry komputerowe (Zainteresowania)", ShowInCv = true }

        };
        private readonly Mock<DbSet<AdditionalInfo>> _mockSet = new Mock<DbSet<AdditionalInfo>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly AdditionalInformationController _controller;


        public AdditionalInformationControllerTests()
        {
            IQueryable<AdditionalInfo> additionalInfosAsQueryable = _additionalInfos.AsQueryable();
            _mockSet.As<IQueryable<AdditionalInfo>>().Setup(m => m.Provider).Returns(additionalInfosAsQueryable.Provider);
            _mockSet.As<IQueryable<AdditionalInfo>>().Setup(m => m.Expression).Returns(additionalInfosAsQueryable.Expression);
            _mockSet.As<IQueryable<AdditionalInfo>>().Setup(m => m.ElementType).Returns(additionalInfosAsQueryable.ElementType);
            _mockSet.As<IQueryable<AdditionalInfo>>().Setup(m => m.GetEnumerator()).Returns(() => additionalInfosAsQueryable.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<AdditionalInfo>())).Callback<AdditionalInfo>((s) => _additionalInfos.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<AdditionalInfo>())).Callback<AdditionalInfo>((s) => _additionalInfos.Remove(s));

            _dbMock.Setup(x => x.SaveChanges()).Returns(1).Verifiable();
            _dbMock.Setup(x => x.AdditionalInfos).Returns(_mockSet.Object);


            AdditionalInformationRepository repository = new AdditionalInformationRepository(_dbMock.Object);
            _controller = new AdditionalInformationController(repository);
        }
        #endregion

        [Fact()]
        public void AdditionalInformationManagementTest()
        {
            //Arrange

            //act
            ViewResult result = _controller.AdditionalInformationManagement() as ViewResult;

            //Assert
            Assert.NotNull(result);

            object model = result.Model;
            Assert.NotNull(model);

            Assert.Equal(string.Empty, result.ViewName);
        }

        [Fact()]
        public void RedirectToAdditionalInformationManagementActionIfRemoveWasCorrect()
        {
            //Arrange

            //Act
            RedirectToRouteResult result = _controller.Remove(1) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("AdditionalInformationManagement", result.RouteValues["action"]);
        }

        [Fact()]
        public void RedirectToErrorPageIfAdditionalInformationDoesNotExist()
        {
            //Arrange
            //Act
            ViewResult result = _controller.Remove(-1) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("ErrorPage", result.ViewName);
        }

    }
}