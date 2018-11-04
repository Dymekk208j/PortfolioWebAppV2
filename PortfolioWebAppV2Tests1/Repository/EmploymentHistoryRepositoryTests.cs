using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using Xunit;

namespace PortfolioWebAppV2Tests1.Repository
{
    public class EmploymentHistoryRepositoryTests
    {
        #region CONFIGURATION
        private readonly List<EmploymentHistory> _employmentHistories = new List<EmploymentHistory>()
        {
            new EmploymentHistory() { EmploymentHistoryId = 1, CompanyName = "Mediadat software", CityOfEmployment="Koszalin", CurrentPlaceOfEmployment = true, Position="Programista", StartDate = new DateTime(2017, 9, 1, 01, 01, 01), ShowInCv = true},
            new EmploymentHistory() { EmploymentHistoryId = 2, CompanyName = "GEIS", CityOfEmployment="Koszalin", CurrentPlaceOfEmployment = false, Position="Brygadzista", StartDate = new DateTime(2016, 1, 1, 01, 01, 01), EndDate = new DateTime(2017, 9, 1, 00, 00, 00), ShowInCv = true}
        };

        private readonly Mock<DbSet<EmploymentHistory>> _mockSet = new Mock<DbSet<EmploymentHistory>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly IRepository<EmploymentHistory, int> _repository;
        #endregion

        public EmploymentHistoryRepositoryTests()
        {
            var employmentHistories = _employmentHistories.AsQueryable();
            _mockSet.As<IQueryable<EmploymentHistory>>().Setup(m => m.Provider).Returns(employmentHistories.Provider);
            _mockSet.As<IQueryable<EmploymentHistory>>().Setup(m => m.Expression).Returns(employmentHistories.Expression);
            _mockSet.As<IQueryable<EmploymentHistory>>().Setup(m => m.ElementType).Returns(employmentHistories.ElementType);
            _mockSet.As<IQueryable<EmploymentHistory>>().Setup(m => m.GetEnumerator()).Returns(() => employmentHistories.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<EmploymentHistory>())).Callback<EmploymentHistory>((s) => _employmentHistories.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<EmploymentHistory>())).Callback<EmploymentHistory>((s) => _employmentHistories.Remove(s));

            _dbMock.Setup(x => x.SaveChanges())
                .Verifiable();
            _dbMock.Setup(x => x.EmploymentHistories).Returns(_mockSet.Object);


            _repository = new EmploymentHistoryRepository(_dbMock.Object);
        }

        [Fact()]
        public void GetTest_existing_EmploymentHistory()
        {
            //Arrange


            //Act
            var result = _repository.Get(1);

            //Assert
            Assert.Equal(result, _employmentHistories[0]);
        }

        [Fact()]
        public void GetTest_nonExistent_EmploymentHistory()
        {
            //Arrange


            //Act
            Action act = () => _repository.Get(-1);

            //Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact()]
        public void GetListTest()
        {
            //Arrange

            //Act
            int result = _repository.GetAll().ToList().Count;

            //Assert
            Assert.Equal(expected: _employmentHistories.Count, actual: result);
        }

        [Theory()]
        [InlineData("Mediadat software", "Koszalin", true, "Programista", "2017-01-01", true)]
        public void AddTest(string companyName, string cityOfEmployment, bool currentPlaceOfEmployment, string position, DateTime startDate, bool showInCv)
        {
            //Arrange
            EmploymentHistory employmentHistory = new EmploymentHistory()
            {
                CompanyName = companyName,
                CityOfEmployment = cityOfEmployment,
                CurrentPlaceOfEmployment = currentPlaceOfEmployment,
                Position = position,
                StartDate = startDate,
                ShowInCv = showInCv
            };


            //Act
            _repository.Add(employmentHistory);


            var result = _repository.GetAll().FirstOrDefault(a =>
                a.CompanyName == companyName && a.CityOfEmployment == cityOfEmployment && a.Position == position);

            //assert
            Assert.NotNull(result);

        }

        [Theory()]
        [InlineData("Testowa nazwa firmy", "Koszalin", true, "Programista", "2017-01-01", true)]
        public void UpdateTest(string companyName, string cityOfEmployment, bool currentPlaceOfEmployment, string position, DateTime startDate, bool showInCv)
        {
            //Arrange
            EmploymentHistory employmentHistory = new EmploymentHistory()
            {
                EmploymentHistoryId = 1,
                CompanyName = companyName,
                CityOfEmployment = cityOfEmployment,
                CurrentPlaceOfEmployment = currentPlaceOfEmployment,
                Position = position,
                StartDate = startDate,
                ShowInCv = showInCv
            };

            //Act 
            _repository.Update(employmentHistory);

            //Assert
            Assert.Equal(actual: _employmentHistories[0].CompanyName, expected: companyName);
            _dbMock.Verify();
        }

        [Fact()]
        public void UpdateTest_nonexistent_object()
        {
            //Arrange
            EmploymentHistory employmentHistory = new EmploymentHistory()
            {
                EmploymentHistoryId = -1,
            };

            //Act 
            var result = _repository.Update(employmentHistory);

            //Assert
            Assert.False(result);
        }

        [Fact()]
        public void RemoveTest_Correct_data_should_return_1()
        {
            //Arrange 
            int exceptedResult = _employmentHistories.Count() - 1;
            var achievementToRemove = _employmentHistories[0];

            //Act
            _repository.Remove(achievementToRemove);
            var result = _repository.GetAll().Count();

            //Assert
            Assert.Equal(expected: exceptedResult, actual: result);
        }

        [Fact()]
        public void RemoveTest_nonExistent_employmentHistory()
        {
            //Arrange
            var nonExistentEmploymentHistory = new EmploymentHistory()
            {
                EmploymentHistoryId = -1,
            };

            //Act
            Action act = () => _repository.Remove(nonExistentEmploymentHistory);

            //Assert
            Assert.Throws<InvalidOperationException>(act);
        }
    }


}
