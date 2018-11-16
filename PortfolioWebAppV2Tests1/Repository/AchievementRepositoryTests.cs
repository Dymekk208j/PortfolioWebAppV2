﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using Xunit;

namespace PortfolioWebAppV2Tests1.Repository
{
    public class AchievementRepositoryTests
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
        private readonly IRepository<Achievement, int> _repository;
        #endregion

        public AchievementRepositoryTests()
        {
            var achievements = _achievements.AsQueryable();
            _mockSet.As<IQueryable<Achievement>>().Setup(m => m.Provider).Returns(achievements.Provider);
            _mockSet.As<IQueryable<Achievement>>().Setup(m => m.Expression).Returns(achievements.Expression);
            _mockSet.As<IQueryable<Achievement>>().Setup(m => m.ElementType).Returns(achievements.ElementType);
            _mockSet.As<IQueryable<Achievement>>().Setup(m => m.GetEnumerator()).Returns(() => achievements.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<Achievement>())).Callback<Achievement>((s) => _achievements.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<Achievement>())).Callback<Achievement>((s) => _achievements.Remove(s));

            _dbMock.Setup(x => x.SaveChanges())
                .Verifiable();
            _dbMock.Setup(x => x.Achievements).Returns(_mockSet.Object);


            _repository = new AchievementRepository(_dbMock.Object);
        }

        [Theory()]
        [InlineData("Tytuł", "opis", "2000-01-01", false)]
        [InlineData("Tytuł3", "op4is", "1990-12-31", true)]
        [InlineData("Długi tytuł", "Normalny opis", "2000-5-7", false)]
        public void AddTest(string title, string description, DateTime date, bool showInCv)
        {
            //Arrange
            Achievement achievement = new Achievement()
            {
                Title = title,
                Description = description,
                Date = date,
                ShowInCv = showInCv
            };

            //Act
            _repository.Add(achievement);


            var result = _repository.GetAll().FirstOrDefault(a =>
                a.Title == title && a.Description == description && a.Date == date && a.ShowInCv == showInCv);

            //assert
            Assert.NotNull(result);

        }

        [Fact()]
        public void GetTest_existing_achievement()
        {
            //Arrange


            //Act
            var result = _repository.Get(1);

            //Assert
            Assert.Equal(result, _achievements[0]);
        }

        [Fact()]
        public void GetTest_nonExistent_achievement()
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
            Assert.Equal(expected: _achievements.Count, actual: result);
        }

        [Theory()]
        [InlineData("Nowy tytuł", "Nowy opis", "2018-11-04", false)]
        public void UpdateTest(string title, string description, DateTime date, bool showInCv)
        {
            //Arrange
            var updatedAchievement = new Achievement()
            {
                AchievementId = 1,
                Title = title,
                Description = description,
                Date = date,
                ShowInCv = showInCv
            };

            //Act 
            _repository.Update(updatedAchievement);

            //Assert
            Assert.Equal(actual: _achievements[0].Title, expected: title);
            _dbMock.Verify();
        }

        [Fact()]
        public void UpdateTest_nonexistent_object()
        {
            //Arrange
            var achievement = new Achievement()
            {
                AchievementId = -1,
            };

            //Act 
            var result = _repository.Update(achievement);

            //Assert
            Assert.False(result);
        }

        [Fact()]
        public void RemoveTest_Correct_data_should_return_1()
        {
            //Arrange 
            var achievementToRemove = _achievements[0];

            //Act
            _repository.Remove(achievementToRemove);
            var result = _repository.GetAll().Count();

            //Assert
            Assert.Equal(expected:1, actual:result);
        }

        [Fact()]
        public void RemoveTest_nonExistent_achievement()
        {
            //Arrange
            var nonExistentAchievement = new Achievement()
            {
                AchievementId = -1
            };

            //Act
            bool result = _repository.Remove(nonExistentAchievement);

            //Assert
            Assert.False(result);
        }
    }
}   