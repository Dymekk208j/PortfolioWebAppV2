using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Moq;
using PortfolioWebAppV2.Controllers;
using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using Xunit;
using Image = PortfolioWebAppV2.Models.DatabaseModels.Image;

namespace PortfolioWebAppV2.Tests.Unit.Controllers
{
    public class ImageControllerTests
    {
        #region CONFIGURATION
        private readonly List<Image> _images = new List<Image>
        {
            new Image() { ImageId = 1, Favorite = true, FileName = "Image1", Guid = "123-123-123-123", ImageType = ImageType.Icon},
            new Image() { ImageId = 2, Favorite = false, FileName = "Image2", Guid = "123-123-123-124", ImageType = ImageType.Icon},
            new Image() { ImageId = 3, Favorite = false, FileName = "Image3", Guid = "123-123-123-125", ImageType = ImageType.ScreenShot},
            new Image() { ImageId = 4, Favorite = true, FileName = "Image4", Guid = "123-123-123-126", ImageType = ImageType.ScreenShot}
        };

        private readonly Mock<DbSet<Image>> _mockSet = new Mock<DbSet<Image>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly ImageController _controller;


        public ImageControllerTests()
        {
            IQueryable<Image> images = _images.AsQueryable();
            _mockSet.As<IQueryable<Image>>().Setup(m => m.Provider).Returns(images.Provider);
            _mockSet.As<IQueryable<Image>>().Setup(m => m.Expression).Returns(images.Expression);
            _mockSet.As<IQueryable<Image>>().Setup(m => m.ElementType).Returns(images.ElementType);
            _mockSet.As<IQueryable<Image>>().Setup(m => m.GetEnumerator()).Returns(() => images.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<Image>())).Callback<Image>((s) => _images.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<Image>())).Callback<Image>((s) => _images.Remove(s));

            _dbMock.Setup(x => x.SaveChanges()).Returns(1).Verifiable();
            _dbMock.Setup(x => x.Images).Returns(_mockSet.Object);


            ImageRepository repository = new ImageRepository(_dbMock.Object);
            _controller = new ImageController(repository);
        }
        #endregion

    
    }
}