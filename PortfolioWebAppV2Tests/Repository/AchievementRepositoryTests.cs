
using System;
using System.Data.Entity;
using Moq;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using Xunit;

namespace PortfolioWebAppV2Tests.Repository
{
    public class AchievementRepositoryTests
    { 
        [Fact()]
        public void GetTest()
        {
            //var mockSet = new Mock<DbSet<Achievement>>();
            //var mockContext = new Mock<ApplicationDbContext>();

            //mockContext.Setup(m => m.Achievements).Returns(mockSet.Object);
            //IRepository<Achievement, int> _repository = new AchievementRepository(mockContext.Object);

            //System.Console.WriteLine("'tt");
            //Achievement achievement = new Achievement(){Date = DateTime.Now, Title = "test", Description = "testdesc", ShowInCv = false};

            //mockSet.Verify(m => m.Add(It.IsAny<Achievement>()), Times.Once);
            ////mockContext.Verify(m => m.SaveChanges(), Times.Once);
            //mockContext.Verify(m => m.SaveChanges(), Times.Once());

            // Arrange
            var userContextMock = new Mock<ApplicationDbContext>();
            var usersMock = new Mock<DbSet<Achievement>>();

            //usersMock.Setup(x => x.Add(It.IsAny<Achievement>())).Returns((Achievement) u) => u);
            usersMock.Setup(x => x.Add(It.IsAny<Achievement>())).Returns((Achievement u) => u);
            userContextMock.Setup(x => x.Achievements).Returns(usersMock.Object);

            IRepository<Achievement, int> _repository = new AchievementRepository(userContextMock.Object);

            Achievement achievement = new Achievement(){Date = DateTime.Now, Title = "test", Description = "testdesc", ShowInCv = false};

            _repository.Add(achievement);

            // Assert
            Assert.NotNull(achievement);
            usersMock.Verify(
                x =>
                    x.Add(
                        It.Is<Achievement>(
                            u =>
                                u.Title == "test" && u.Description == "testdesc" && u.ShowInCv == false )), Times.Once);
            userContextMock.Verify(x => x.SaveChanges(), Times.Once);

            ApplicationDbContext context = new ApplicationDbContext();
        

    }

        //[Fact()]
        //public void GetTest1()
        //{
        //    throw new NotImplementedException();
        //}

        //[Fact()]
        //public void AddTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[Fact()]
        //public void RemoveTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[Fact()]
        //public void UpdateTest()
        //{
        //    throw new NotImplementedException();
        //}

        #region CONFIGURATION


        #endregion
    }


}