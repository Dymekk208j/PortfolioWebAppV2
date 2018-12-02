using Xunit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2Tests1.Repository
{
    public class PrivateInformationRepositoryTests
    {
        #region CONFIGURATION
        private readonly List<PrivateInformation> _privateInformations = new List<PrivateInformation>()
        {
            new PrivateInformation() {
                PrivateInformationId = 1,
                City = "Koszalin",
                Email = "Kontakt@DamianDziura.pl",
                FirstName = "Damian",
                LastName = "Dziura",
                Street = "1 maja",
                HouseNumber = "1",
                FlatNumber = "3",
                PostCode ="75-800",
                HomePage = "www.DamianDziura.pl",
                PhoneNumber ="510-075-067",
                ImageLink = "http://via.placeholder.com/150x180"
            }
        };
        private readonly Mock<DbSet<PrivateInformation>> _mockSet = new Mock<DbSet<PrivateInformation>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly IRepository<PrivateInformation, int> _repository;
        #endregion

        public PrivateInformationRepositoryTests()
        {
            IQueryable<PrivateInformation> privateInformations = _privateInformations.AsQueryable();
            _mockSet.As<IQueryable<PrivateInformation>>().Setup(m => m.Provider).Returns(privateInformations.Provider);
            _mockSet.As<IQueryable<PrivateInformation>>().Setup(m => m.Expression).Returns(privateInformations.Expression);
            _mockSet.As<IQueryable<PrivateInformation>>().Setup(m => m.ElementType).Returns(privateInformations.ElementType);
            _mockSet.As<IQueryable<PrivateInformation>>().Setup(m => m.GetEnumerator()).Returns(() => privateInformations.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<PrivateInformation>())).Callback<PrivateInformation>((s) => _privateInformations.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<PrivateInformation>())).Callback<PrivateInformation>((s) => _privateInformations.Remove(s));

            _dbMock.Setup(x => x.SaveChanges())
                .Verifiable();
            _dbMock.Setup(x => x.PrivateInformations).Returns(_mockSet.Object);


            _repository = new PrivateInformationRepository(_dbMock.Object);
        }

        [Fact()]
        public void GetTest_existing_PrivateInformation()
        {
            //Arrange


            //Act
            PrivateInformation result = _repository.Get(1);

            //Assert
            Assert.Equal(result, _privateInformations[0]);
        }

        [Fact()]
        public void GetTest_nonExistent_PrivateInformation()
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
            Assert.Equal(expected: _privateInformations.Count, actual: result);
        }

        [Fact()]
        public void AddTest()
        {
            //Arrange
            PrivateInformation privateInformation = new PrivateInformation()
            {
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
            _repository.Add(privateInformation);


            PrivateInformation result = _repository.GetAll().FirstOrDefault(a =>
                a.City == privateInformation.City && a.FirstName == privateInformation.FirstName && a.LastName == privateInformation.LastName);

            //assert
            Assert.NotNull(result);

        }

        [Fact()]
        public void UpdateTest()
        {
            //Arrange
            PrivateInformation privateInformation = new PrivateInformation()
            {
                PrivateInformationId = 1,
                City = "Nowe miasto",
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
            _repository.Update(privateInformation);

            //Assert
            Assert.Equal(actual: _privateInformations[0].City, expected: privateInformation.City);
            _dbMock.Verify();
        }

        [Fact()]
        public void UpdateTest_nonexistent_object()
        {
            //Arrange
            PrivateInformation privateInformation = new PrivateInformation()
            {
                PrivateInformationId = -1
            };

            //Act 
            bool result = _repository.Update(privateInformation);

            //Assert
            Assert.False(result);
        }

        [Fact()]
        public void RemoveTest_Correct_data_should_return_1()
        {
            //Arrange 
            int exceptedResult = _privateInformations.Count() - 1;
            PrivateInformation achievementToRemove = _privateInformations[0];

            //Act
            _repository.Remove(achievementToRemove);
            int result = _repository.GetAll().Count();

            //Assert
            Assert.Equal(expected: exceptedResult, actual: result);
        }

        [Fact()]
        public void RemoveTest_nonExistent_PrivateInformation()
        {
            //Arrange
            PrivateInformation nonExistentPrivateInformation = new PrivateInformation()
            {
                PrivateInformationId = -1
            };

            //Act
            bool result = _repository.Remove(nonExistentPrivateInformation);

            //Assert
            Assert.False(result);
        }

    }
}