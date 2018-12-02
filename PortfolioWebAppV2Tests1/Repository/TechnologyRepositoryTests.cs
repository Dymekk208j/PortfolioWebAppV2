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
    public class TechnologyRepositoryTests
    {
        #region CONFIGURATION
        private readonly List<Technology> _technologies = new List<Technology>()
        {
            new Technology() {TechnologyId = 1, Name="JAVA", KnowledgeLevel = Technology.LevelOfKnowledge.Ok, ShowInCv = true },
            new Technology() {TechnologyId = 2, Name="C#", KnowledgeLevel= Technology.LevelOfKnowledge.VeryWell, ShowInCv = true },
            new Technology() {TechnologyId = 3, Name="JavaScript", KnowledgeLevel=Technology.LevelOfKnowledge.Well, ShowInCv = false }
        };
        private readonly Mock<DbSet<Technology>> _mockSet = new Mock<DbSet<Technology>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly IRepository<Technology, int> _repository;
        #endregion

        public TechnologyRepositoryTests()
        {
            IQueryable<Technology> technologies = _technologies.AsQueryable();
            _mockSet.As<IQueryable<Technology>>().Setup(m => m.Provider).Returns(technologies.Provider);
            _mockSet.As<IQueryable<Technology>>().Setup(m => m.Expression).Returns(technologies.Expression);
            _mockSet.As<IQueryable<Technology>>().Setup(m => m.ElementType).Returns(technologies.ElementType);
            _mockSet.As<IQueryable<Technology>>().Setup(m => m.GetEnumerator()).Returns(() => technologies.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<Technology>())).Callback<Technology>((s) => _technologies.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<Technology>())).Callback<Technology>((s) => _technologies.Remove(s));

            _dbMock.Setup(x => x.SaveChanges())
                .Verifiable();
            _dbMock.Setup(x => x.Technologies).Returns(_mockSet.Object);


            _repository = new TechnologyRepository(_dbMock.Object);
        }

        [Fact()]
        public void GetTest_existing_Technology()
        {
            //Arrange


            //Act
            Technology result = _repository.Get(1);

            //Assert
            Assert.Equal(result, _technologies[0]);
        }

        [Fact()]
        public void GetTest_nonExistent_Technology()
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
            Assert.Equal(expected: _technologies.Count, actual: result);
        }

        [Theory()]
        [InlineData("JAVA", Technology.LevelOfKnowledge.Ok, true)]
        [InlineData("JavaScript", Technology.LevelOfKnowledge.Well, true)]
        [InlineData("C#", Technology.LevelOfKnowledge.VeryWell, false)]
        public void AddTest(string name, Technology.LevelOfKnowledge levelOfKnowledge, bool showInCv)
        {
            //Arrange
            Technology technology = new Technology()
            {
                Name = name,
                KnowledgeLevel = levelOfKnowledge,
                ShowInCv = showInCv
            };

            //Act
            _repository.Add(technology);


            Technology result = _repository.GetAll().FirstOrDefault(a =>
                a.Name == technology.Name && a.KnowledgeLevel == technology.KnowledgeLevel && a.ShowInCv == technology.ShowInCv);

            //assert
            Assert.NotNull(result);

        }

        [Theory()]
        [InlineData("Nowy tytuł", Technology.LevelOfKnowledge.Ok, true)]
        public void UpdateTest(string name, Technology.LevelOfKnowledge levelOfKnowledge, bool showInCv)
        {
            //Arrange
            Technology technology = new Technology()
            {
                TechnologyId = 1,
                Name = name,
                KnowledgeLevel = levelOfKnowledge,
                ShowInCv = showInCv
            };

            //Act 
            _repository.Update(technology);

            //Assert
            Assert.Equal(actual: _technologies[0].Name, expected: name);
            _dbMock.Verify();
        }

        [Fact()]
        public void UpdateTest_nonexistent_object()
        {
            //Arrange
            Technology technology = new Technology()
            {
                TechnologyId = -1
            };

            //Act 
            bool result = _repository.Update(technology);

            //Assert
            Assert.False(result);
        }

        [Fact()]
        public void RemoveTest_Correct_data_should_return_1()
        {
            //Arrange 
            int exceptedResult = _technologies.Count() - 1;
            Technology achievementToRemove = _technologies[0];

            //Act
            _repository.Remove(achievementToRemove);
            int result = _repository.GetAll().Count();

            //Assert
            Assert.Equal(expected: exceptedResult, actual: result);
        }

        [Fact()]
        public void RemoveTest_nonExistent_Technology()
        {
            //Arrange
            Technology nonExistentAdditionalInfo = new Technology()
            {
                TechnologyId = -1
            };

            //Act
            bool result = _repository.Remove(nonExistentAdditionalInfo);

            //Assert
            Assert.False(result);
        }
    }
}