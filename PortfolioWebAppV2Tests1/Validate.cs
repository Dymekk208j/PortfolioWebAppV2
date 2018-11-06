using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PortfolioWebAppV2Tests1
{
    public static class Validate
    {
        public static void ValidateModel(Controller controller, object viewModel)
        {
            controller.ModelState.Clear();

            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResult = new List<ValidationResult>();

            Validator.TryValidateObject(viewModel, validationContext, validationResult, true);
            foreach (var result in validationResult)
            {
                foreach (var name in result.MemberNames)
                {
                    controller.ModelState.AddModelError(name, result.ErrorMessage);
                }
            }
        }
    }
}
