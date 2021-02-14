using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kerber.SpotifyLibrary.WebApi.Configs
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                if (context.Result is ObjectResult)
                {
                    ((ObjectResult)context.Result).Value = new { Valid = false, Message = context.Result };
                }
            }
        }
    }
}

