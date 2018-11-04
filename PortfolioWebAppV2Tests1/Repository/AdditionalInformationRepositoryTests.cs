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
    public class AdditionalInformationRepositoryTests
    {
        #region CONFIGURATION
        private readonly List<AdditionalInfo> _additionalInfos = new List<AdditionalInfo>()
        {
           new AdditionalInfo()
           {
               AdditionalInfoId = 1,
               ShowInCv = true,
               Title = "Pierwszy tytuł",
               Type = AdditionalInfo.TypeOfAddtionalInfo.AdditionalSkills
           },
            new AdditionalInfo()
            {
                AdditionalInfoId = 2,
                ShowInCv = true,
                Title = "Drugi tytuł",
                Type = AdditionalInfo.TypeOfAddtionalInfo.ForeignLanguages
            },
            new AdditionalInfo()
            {
                AdditionalInfoId = 3,
                ShowInCv = false,
                Title = "Trzeci tytuł",
                Type = AdditionalInfo.TypeOfAddtionalInfo.Interests
            }
        };
        private readonly Mock<DbSet<AdditionalInfo>> _mockSet = new Mock<DbSet<AdditionalInfo>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly IRepository<AdditionalInfo, int> _repository;
        #endregion

        public AdditionalInformationRepositoryTests()
        {
            var additionalInfos = _additionalInfos.AsQueryable();
            _mockSet.As<IQueryable<AdditionalInfo>>().Setup(m => m.Provider).Returns(additionalInfos.Provider);
            _mockSet.As<IQueryable<AdditionalInfo>>().Setup(m => m.Expression).Returns(additionalInfos.Expression);
            _mockSet.As<IQueryable<AdditionalInfo>>().Setup(m => m.ElementType).Returns(additionalInfos.ElementType);
            _mockSet.As<IQueryable<AdditionalInfo>>().Setup(m => m.GetEnumerator()).Returns(() => additionalInfos.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<AdditionalInfo>())).Callback<AdditionalInfo>((s) => _additionalInfos.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<AdditionalInfo>())).Callback<AdditionalInfo>((s) => _additionalInfos.Remove(s));

            _dbMock.Setup(x => x.SaveChanges())
                .Verifiable();
            _dbMock.Setup(x => x.AdditionalInfos).Returns(_mockSet.Object);


            _repository = new AdditionalInformationRepository(_dbMock.Object);
        }

        [Fact()]
        public void GetTest_existing_additionalInfos()
        {
            //Arrange


            //Act
            var result = _repository.Get(1);

            //Assert
            Assert.Equal(result, _additionalInfos[0]);
        }

        [Fact()]
        public void GetTest_nonExistent_additionalInfos()
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
            Assert.Equal(expected: _additionalInfos.Count, actual: result);
        }

        [Theory()]
        [InlineData("Pierwszy tytuł", false, AdditionalInfo.TypeOfAddtionalInfo.AdditionalSkills)]
        [InlineData("Drugi tytul", true, AdditionalInfo.TypeOfAddtionalInfo.ForeignLanguages)]
        [InlineData("Trzeci tytuł", false, AdditionalInfo.TypeOfAddtionalInfo.Interests)]
        public void AddTest(string title, bool showInCv, AdditionalInfo.TypeOfAddtionalInfo typeOfAddtionalInfo)
        {
            //Arrange
            AdditionalInfo additionalInfo = new AdditionalInfo()
            {
                Title = title,
                ShowInCv = showInCv,
                Type = typeOfAddtionalInfo
            };

            //Act
            _repository.Add(additionalInfo);


            var result = _repository.GetAll().FirstOrDefault(a =>
                a.Title == title && a.ShowInCv == showInCv && a.Type == typeOfAddtionalInfo);

            //assert
            Assert.NotNull(result);

        }

        [Theory()]
        [InlineData("Nowy tytuł", false, AdditionalInfo.TypeOfAddtionalInfo.AdditionalSkills)]
        public void UpdateTest(string title, bool showInCv, AdditionalInfo.TypeOfAddtionalInfo type)
        {
            //Arrange
            AdditionalInfo additionalInfo = new AdditionalInfo()
            {
                AdditionalInfoId = 1,
                Title = title,
                ShowInCv = showInCv,
                Type = type
            };

            //Act 
            _repository.Update(additionalInfo);

            //Assert
            Assert.Equal(actual: _additionalInfos[0].Title, expected: title);
            _dbMock.Verify();
        }

        [Fact()]
        public void UpdateTest_nonexistent_object()
        {
            //Arrange
            AdditionalInfo additionalInfo = new AdditionalInfo()
            {
                AdditionalInfoId = -1,
                Title = "title",
                ShowInCv = false,
                Type = AdditionalInfo.TypeOfAddtionalInfo.AdditionalSkills
            };

            //Act 
            var result = _repository.Update(additionalInfo);

            //Assert
            Assert.False(result);
        }

        [Fact()]
        public void RemoveTest_Correct_data_should_return_1()
        {
            //Arrange 
            int exceptedResult = _additionalInfos.Count()-1;
            var achievementToRemove = _additionalInfos[0];

            //Act
            _repository.Remove(achievementToRemove);
            var result = _repository.GetAll().Count();

            //Assert
            Assert.Equal(expected: exceptedResult, actual: result);
        }

        [Fact()]
        public void RemoveTest_nonExistent_achievement()
        {
            //Arrange
            var nonExistentAdditionalInfo = new AdditionalInfo()
            {
                AdditionalInfoId = -1,
                Title = "",
                ShowInCv = false,
                Type = AdditionalInfo.TypeOfAddtionalInfo.AdditionalSkills
            };

            //Act
            Action act = () => _repository.Remove(nonExistentAdditionalInfo);

            //Assert
            Assert.Throws<InvalidOperationException>(act);
        }

    }
}