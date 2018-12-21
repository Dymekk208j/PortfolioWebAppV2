using Xunit;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Moq;
using PortfolioWebAppV2.Controllers;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Tests.Unit.Controllers
{
    public class PrivateInformationControllerTests
    {
        #region CONFIGURATION
        private readonly List<PrivateInformation> _list = new List<PrivateInformation>()
        {
            new PrivateInformation() { PrivateInformationId = 1, City = "Koszalin", Email = "Kontakt@DamianDziura.pl", FirstName = "Damian", LastName = "Dziura"
                , Street = "1 maja", HouseNumber = "1", FlatNumber
                    = "3", PostCode="75-800", HomePage = "www.DamianDziura.pl", PhoneNumber="510-075-067", ImageLink= "http://via.placeholder.com/150x180" }
        };
        private readonly Mock<DbSet<PrivateInformation>> _mockSet = new Mock<DbSet<PrivateInformation>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly PrivateInformationController _controller;

        public PrivateInformationControllerTests()
        {
            IQueryable<PrivateInformation> information = _list.AsQueryable();
            _mockSet.As<IQueryable<PrivateInformation>>().Setup(m => m.Provider).Returns(information.Provider);
            _mockSet.As<IQueryable<PrivateInformation>>().Setup(m => m.Expression).Returns(information.Expression);
            _mockSet.As<IQueryable<PrivateInformation>>().Setup(m => m.ElementType).Returns(information.ElementType);
            _mockSet.As<IQueryable<PrivateInformation>>().Setup(m => m.GetEnumerator()).Returns(() => information.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<PrivateInformation>())).Callback<PrivateInformation>((s) => _list.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<PrivateInformation>())).Callback<PrivateInformation>((s) => _list.Remove(s));

            _dbMock.Setup(x => x.SaveChanges()).Returns(1).Verifiable();
            _dbMock.Setup(x => x.PrivateInformations).Returns(_mockSet.Object);


            PrivateInformationRepository repository = new PrivateInformationRepository(_dbMock.Object);
            _controller = new PrivateInformationController(repository);
        }
        #endregion

        [Fact()]
        public void UpdateTest_RedirectToActionEditCvInAdminPanelControllerIfModelStateIsValid()
        {
            //Arrange
            PrivateInformation privateInformation = new PrivateInformation()
            {
                PrivateInformationId = 1,
                City = "Koszalin",
                Email = "Kontakt@DamianDziura.pl",
                FirstName = "Damian",
                LastName = "Dziura",
                Street = "1 maja",
                HouseNumber = "1",
                FlatNumber = "3",
                PostCode = "75-800",
                HomePage = "www.DamianDziura.pl",
                PhoneNumber = "510-075-067",
                ImageLink = "http://via.placeholder.com/150x180"
            };

            //Act
            RedirectToRouteResult result = _controller.Update(privateInformation) as RedirectToRouteResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal("EditCv", result.RouteValues["action"]);
            Assert.Equal("AdminPanel", result.RouteValues["controller"]);
        }

        [Fact()]
        public void UpdateTest_RedirectToErrorPageIfModelStateIsNotValid()
        {
            //Arrange
            PrivateInformation privateInformation = new PrivateInformation()
            {
                PrivateInformationId = 1,
                City = "Koszalin",
                Email = "Kontakt@DamianDziura.pl",
                FirstName = null,
                LastName = null,
                Street = "1 maja",
                HouseNumber = "1",
                FlatNumber = "3",
                PostCode = "75-800",
                HomePage = "www.DamianDziura.pl",
                PhoneNumber = "510-075-067",
                ImageLink = "http://via.placeholder.com/150x180"
            };
            
            //Act
            Validate.ValidateModel(_controller, privateInformation);
            ViewResult result = _controller.Update(privateInformation) as ViewResult;

            //Assert
            Assert.NotNull(result);
            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.Equal("ErrorPage", result.ViewName);
        }
    }
}