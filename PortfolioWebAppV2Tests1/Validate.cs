using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PortfolioWebAppV2.Tests.Unit
{
    public static class Validate
    {
        public static void ValidateModel(Controller controller, object viewModel)
        {
            controller.ModelState.Clear();

            ValidationContext validationContext = new ValidationContext(viewModel, null, null);
            List<ValidationResult> validationResult = new List<ValidationResult>();

            Validator.TryValidateObject(viewModel, validationContext, validationResult, true);
            foreach (ValidationResult result in validationResult)
            {
                foreach (string name in result.MemberNames)
                {
                    controller.ModelState.AddModelError(name, result.ErrorMessage);
                }
            }
        }
    }
}
