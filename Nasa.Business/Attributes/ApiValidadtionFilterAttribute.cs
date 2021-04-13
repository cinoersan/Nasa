using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Nasa.Business.Exceptions;

namespace Nasa.Business.Attributes
{
    public class ApiValidadtionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            var errors = context.ModelState.Values.SelectMany(t => t.Errors, (n, m) => m.ErrorMessage);
            var error = errors.FirstOrDefault();
            throw new BadRequestException(error);
        }
    }
}
