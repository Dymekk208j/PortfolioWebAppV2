using Xunit;
using PortfolioWebAppV2.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using PortfolioWebAppV2.Models.DatabaseModels;

namespace PortfolioWebAppV2.Tests.Unit.Repository
{
    public class ContactRepositoryTests
    {
        #region CONFIGURATION
        private readonly List<Contact> _contacts = new List<Contact>()
        {
            new Contact()
            {
                ContactId = 1,
                Email1 = "kontakt@DamianDziura.pl",
                Email2 = "Dymekk208j@gmail.com",
                FacebookLink = "https://www.facebook.com/dymekk208j",
                GitHubLink = "https://github.com/Dymekk208j/",
                LinkedInLink = "https://www.linkedin.com/in/damian-dziura-27a821114/",
                PhoneNumber = "+48 510-075-067"
            }
        };
        private readonly Mock<DbSet<Contact>> _mockSet = new Mock<DbSet<Contact>>();
        private readonly Mock<ApplicationDbContext> _dbMock = new Mock<ApplicationDbContext>();
        private readonly IRepository<Contact, int> _repository;
        #endregion

        public ContactRepositoryTests()
        {
            IQueryable<Contact> contacts = _contacts.AsQueryable();
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(contacts.Provider);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(contacts.Expression);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(contacts.ElementType);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => contacts.GetEnumerator());
            _mockSet.Setup(d => d.Add(It.IsAny<Contact>())).Callback<Contact>((s) => _contacts.Add(s));
            _mockSet.Setup(d => d.Remove(It.IsAny<Contact>())).Callback<Contact>((s) => _contacts.Remove(s));

            _dbMock.Setup(x => x.SaveChanges())
                .Verifiable();
            _dbMock.Setup(x => x.Contacts).Returns(_mockSet.Object);


            _repository = new ContactRepository(_dbMock.Object);
        }

        [Fact()]
        public void AddTest()
        {
            //Arrange
            Contact contact = new Contact()
            {
                ContactId = 1,
                Email1 = "kontakt@DamianDziura.pl",
                Email2 = "Dymekk208j@gmail.com",
                FacebookLink = "https://www.facebook.com/dymekk208j",
                GitHubLink = "https://github.com/Dymekk208j/",
                LinkedInLink = "https://www.linkedin.com/in/damian-dziura-27a821114/",
                PhoneNumber = "+48 510-075-067"
            };

            //Act
            _repository.Add(contact);


            Contact result = _repository.GetAll().FirstOrDefault(a =>
                a.Email1 == contact.Email1 && a.Email2 == contact.Email2 && a.GitHubLink == contact.GitHubLink && a.ContactId == contact.ContactId);

            //assert
            Assert.NotNull(result);

        }

        [Fact()]
        public void GetTest_existing_contact()
        {
            //Arrange


            //Act
            Contact result = _repository.Get(1);

            //Assert
            Assert.Equal(result, _contacts[0]);
        }

        [Fact()]
        public void GetTest_nonExistent_contact()
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
            Assert.Equal(expected: _contacts.Count, actual: result);
        }

        [Fact()]
        public void UpdateTest()
        {
            //Arrange
            Contact updatedContact = new Contact()
            {
                ContactId = 1,
                Email1 = "kontakt@DamianDziura.plv2",
                Email2 = "Dymekk208j@gmail.comv2",
                FacebookLink = "https://www.facebook.com/dymekk208j",
                GitHubLink = "https://github.com/Dymekk208j/",
                LinkedInLink = "https://www.linkedin.com/in/damian-dziura-27a821114/",
                PhoneNumber = "+48 510-075-067"
            };

            //Act 
            _repository.Update(updatedContact);

            //Assert
            Assert.Equal(actual: _contacts[0].Email1, expected: updatedContact.Email1);
            _dbMock.Verify();
        }

        [Fact()]
        public void UpdateTest_nonexistent_object()
        {
            //Arrange
            Contact nonExistingContact = new Contact()
            {
                ContactId = -1,
            };

            //Act 
            bool result = _repository.Update(nonExistingContact);

            //Assert
            Assert.False(result);
        }

        [Fact()]
        public void RemoveTest_Correct_data_should_return_1()
        {
            //Arrange 
            Contact achievementToRemove = _contacts[0];
            int exceptedResult = _contacts.Count - 1;
            //Act
            _repository.Remove(achievementToRemove);
            int result = _repository.GetAll().Count();

            //Assert
            Assert.Equal(expected: exceptedResult, actual: result);
        }

        [Fact()]
        public void RemoveTest_nonExistent_achievement()
        {
            //Arrange
            Contact nonExistingContact = new Contact()
            {
                ContactId = -1,
            };

            //Act
            bool result = _repository.Remove(nonExistingContact);

            //Assert
            Assert.False(result);
        }
    }
}