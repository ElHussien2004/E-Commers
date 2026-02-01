
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Shared.ErrorModels;

namespace E_Commers.Factories
{
    public  static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrors (ActionContext Context)
        {
            var Error = Context.ModelState.Where(M => M.Value.Errors.Any())
                   .Select(M => new Shared.ErrorModels.ValidationError()
                   {
                       Faild = M.Key,
                       Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                   });

            var Res = new ValidationErrorToReturn()
            {
                validationErrors = Error
            };
            return new BadRequestObjectResult(Res);
        }
    }
}
