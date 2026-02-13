using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Comun.Models;

namespace Comun.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errores = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message ?? "Valor inválido" : e.ErrorMessage)
                    .ToList();

                var response = new ResponseModel<object>
                {
                    Exito = false,
                    Mensaje = "Ha ocurrido un error.",
                    Data = null,
                    Errores = errores
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
