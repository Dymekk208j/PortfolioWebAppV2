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
    public class EducationRepositoryTests
    {
        #region CONFIGURATION
        private readonly List<Education> _educations = new List<Education>()
        {
            new Education() { EducationId = 1, CurrentPlaceOfEducation = true, SchooleName = "Politechnika koszalinska", Department = "WYDZIAŁ ELEKTRONIKI I INFORMATYKI", Specialization = "Inżynieria testów oprogramowania", EndDate = new DateTime(2020, 1, 1, 01, 01, 01), StartDate = new DateTime(2016, 1, 1, 01, 01, 01), ShowInCv = true},
            new Education() { EducationId = 2, CurrentPlaceOfEducation = false, SchooleName = "ZESPÓŁ SZKÓŁ NR. 9 IM. ROMUALLDA TRAGUTTA", Department = "TECHNIKUM INFORMATYCZNE", Specialization = "Technik informatyk", EndDate = new DateTime(2014, 1, 1, 01, 01, 01), StartDate = new DateTime(2010, 1, 1, 01, 01, 01), ShowInCv = true}
        };
        private readonly Mock<DbSet<Education>> _mockSet = new Mock<DbSet<Education>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly IRepository<Education, int> _repository;
        #endregion

        public EducationRepositoryTests()
        {
            IQueryable<Education> educations = _educations.AsQueryable();
            _mockSet.As<IQueryable<Education>>().Setup(m => m.Provider).Returns(educations.Provider);
            _mockSet.As<IQueryable<Education>>().Setup(m => m.Expression).Returns(educations.Expression);
            _mockSet.As<IQueryable<Education>>().Setup(m => m.ElementType).Returns(educations.ElementType);
            _mockSet.As<IQueryable<Education>>().Setup(m => m.GetEnumerator()).Returns(() => educations.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<Education>())).Callback<Education>((s) => _educations.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<Education>())).Callback<Education>((s) => _educations.Remove(s));

            _dbMock.Setup(x => x.SaveChanges())
                .Verifiable();
            _dbMock.Setup(x => x.Educations).Returns(_mockSet.Object);


            _repository = new EducationRepository(_dbMock.Object);
        }

        [Theory()]
        [InlineData(true, "Politechnika koszalinska", "WYDZIAŁ ELEKTRONIKI I INFORMATYKI", "Inżynieria testów oprogramowania", "2020-01-01", "2016-01-01", true)]
        public void AddTest(bool currentPlaceOfEducation, string schooleName, string department, string specialization, DateTime endDate, DateTime startDate, bool showInCv)
        {
            //Arrange
            Education education = new Education()
            {
                CurrentPlaceOfEducation = currentPlaceOfEducation,
                SchooleName = schooleName,
                Department = department,
                Specialization = specialization,
                EndDate = endDate,
                StartDate = startDate,
                ShowInCv = showInCv
            };
            

                //Act
            _repository.Add(education);


            Education result = _repository.GetAll().FirstOrDefault(a =>
                a.CurrentPlaceOfEducation == currentPlaceOfEducation && a.SchooleName == schooleName && a.Department == department && a.Specialization == specialization);

            //assert
            Assert.NotNull(result);

        }

        [Fact()]
        public void GetTest_existing_education()
        {
            //Arrange


            //Act
            Education result = _repository.Get(1);

            //Assert
            Assert.Equal(result, _educations[0]);
        }

        [Fact()]
        public void GetTest_nonExistent_education()
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
            Assert.Equal(expected: _educations.Count, actual: result);
        }

        [Theory()]
        [InlineData(true, "Testowa nazwa szkoły", "WYDZIAŁ ELEKTRONIKI I INFORMATYKI", "Inżynieria testów oprogramowania", "2020-01-01", "2016-01-01", true)]
        public void UpdateTest(bool currentPlaceOfEducation, string schoolName, string department, string specialization, DateTime endDate, DateTime startDate, bool showInCv)
        {
            //Arrange
            Education updatedEducation = new Education()
            {
                EducationId = 1,
                CurrentPlaceOfEducation = currentPlaceOfEducation,
                SchooleName = schoolName,
                Department = department,
                Specialization = specialization,
                EndDate = endDate,
                StartDate = startDate,
                ShowInCv = showInCv
            };

            //Act 
            _repository.Update(updatedEducation);

            //Assert
            Assert.Equal(actual: _educations[0].SchooleName, expected: schoolName);
            _dbMock.Verify();
        }

        [Fact()]
        public void UpdateTest_nonexistent_object()
        {
            //Arrange
            Education education = new Education()
            {
                EducationId = -1
            };

            //Act 
            bool result = _repository.Update(education);

            //Assert
            Assert.False(result);
        }

        [Fact()]
        public void RemoveTest_Correct_data_should_return_1()
        {
            //Arrange 
            int exceptedResult = _educations.Count() - 1;
            Education achievementToRemove = _educations[0];

            //Act
            _repository.Remove(achievementToRemove);
            int result = _repository.GetAll().Count();

            //Assert
            Assert.Equal(expected: exceptedResult, actual: result);
        }

        [Fact()]
        public void RemoveTest_nonExistent_education()
        {
            //Arrange
            Education nonExistentEducation = new Education()
            {
                EducationId = -1,
            };

            //Act
            bool result = _repository.Remove(nonExistentEducation);

            //Assert
            Assert.False(result);
        }
    }
}