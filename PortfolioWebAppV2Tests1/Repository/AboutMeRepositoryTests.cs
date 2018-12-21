using Xunit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;

namespace PortfolioWebAppV2.Tests.Unit.Repository
{
    public class AboutMeRepositoryTests
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
        private readonly IRepository<AboutMe, int> _repository;
        #endregion

        public AboutMeRepositoryTests()
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


            _repository = new AboutMeRepository(_dbMock.Object);
        }

        [Fact()]
        public void GetListTest()
        {
            //Arrange


            //Act
            int result = _repository.GetAll().ToList().Count;

            //Assert
            Assert.Equal(expected: _aboutMe.Count, actual: result);
        }

        [Fact()]
        public void GetTest_existing_aboutMe()
        {
            //Arrange


            //Act
            AboutMe result = _repository.Get(1);

            //Assert
            Assert.Equal(result, _aboutMe[0]);
        }

        [Fact()]
        public void GetTest_nonExistent_aboutMe()
        {
            //Arrange


            //Act
            Action act = () => _repository.Get(-1);

            //Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Theory()]
        [InlineData("Tytuł", "Text", "http://via.placeholder.com/250x300")]
        public void AddTest(string title, string text, string imageLink)
        {
            //Arrange
            AboutMe aboutMe = new AboutMe()
            {
                AboutMeId = 1,
                ImageLink = imageLink,
                Text = text,
                Title = title
            };

            //Act
            bool result = _repository.Add(aboutMe);

            //assert
            Assert.True(result);

        }

        [Theory()]
        [InlineData("Nowy Tytuł", "NowyText", "http://via.placeholder.com/250x300v2")]
        public void UpdateTest_existing_object(string title, string text, string imageLink)
        {
            //Arrange
            AboutMe updateAboutMe = new AboutMe()
            {
                AboutMeId = 1,
                ImageLink = imageLink,
                Text = text,
                Title = title
            };

            //Act 
            _repository.Update(updateAboutMe);

            //Assert
            Assert.Equal(actual: _aboutMe[0].Title, expected: title);
            _dbMock.Verify();
        }

        [Fact()]
        public void UpdateTest_nonexistent_object()
        {
            //Arrange
            AboutMe updateAboutMe = new AboutMe()
            {
                AboutMeId = -1,
                ImageLink = "imageLink",
                Text = "text",
                Title = "title"
            };

            //Act 
            bool result = _repository.Update(updateAboutMe);

            //Assert
            Assert.False(result);
        }


        [Fact()]
        public void RemoveTest_Correct_data_should_return_1()
        {
            //Arrange 
            AboutMe achievementToRemove = _aboutMe[0];

            //Act
            _repository.Remove(achievementToRemove);
            int result = _repository.GetAll().Count();

            //Assert
            Assert.Equal(expected: 0, actual: result);
        }

        [Fact()]
        public void RemoveTest_nonExistent_aboutMe()
        {
            //Arrange
            AboutMe nonExistentAboutMe = new AboutMe()
            {
                AboutMeId = -1,
                ImageLink = "imageLink",
                Text = "text",
                Title = "title"
            };

            //Act
            bool result = _repository.Remove(nonExistentAboutMe);

            //Assert
            Assert.False(result);
        }
    }

    
}