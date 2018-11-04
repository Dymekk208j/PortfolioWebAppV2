using Xunit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using PortfolioWebAppV2.Models.DatabaseModels;

namespace PortfolioWebAppV2Tests.Controllers
{


    public class AchievementControllerTests
    {
        public static IEnumerable<object[]> IncorrectData()
        {
            yield return new object[]
            {
                "Pierwszy tytuł",
                "Pierwszy opis",
                null
            };
            yield return new object[]
            {
                null,
                "Pierwszy opis",
                new DateTime(2018, 01, 01), 
            };
        }

        [Theory()]
        [InlineData("Tytuł pierwszy", "opis pierwszy", "2017-04-11")]
        [InlineData("DrugiTytuł", "Drugi opis", "2017-01-01")]
        [InlineData("Opis ze znakami $pecjalnym!", "trzeci.op[is", "2017-12-12")]
        public void AddAchievementTest_model_is_valid_should_return_true(string title, string description, DateTime date)
        {
            Achievement achievement = new Achievement()
            {
                AchievementId = 1,
                Date = date,
                Title = title,
                Description = description,
                ShowInCv = false
            };

            Assert.True(ValidateModel(achievement).Count == 0);
        }

        [Theory, MemberData(nameof(IncorrectData))]
        public void AddAchievementTest_model_is_not_valid_should_return_false(string title, string desc, DateTime? date)
        {
            // Assemble
            Achievement achievement = new Achievement()
            {
                AchievementId = 1,
                Date = date,
                Title = title,
                Description = desc,
                ShowInCv = false
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(achievement, new ValidationContext(achievement), validationResults, true);


            // Assert
            Assert.False(actual, "Expected validation to fail.");
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);

            return validationResults;
        }


        #region Configurtation


        #endregion

    }


    

}