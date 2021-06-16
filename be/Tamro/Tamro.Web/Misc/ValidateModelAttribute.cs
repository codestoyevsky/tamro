using Microsoft.AspNetCore.Mvc.Filters;
using Tamro.Web.Exceptions;


namespace Tamro.Web.Misc
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ValidationModelException(context.ModelState);
            }
        }
    }
}
