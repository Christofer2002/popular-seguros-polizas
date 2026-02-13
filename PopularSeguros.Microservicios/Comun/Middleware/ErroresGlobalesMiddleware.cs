using Comun.Models;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace Comun.Middlewares
{
    public class MiddlewareErroresGlobal
    {
        private readonly RequestDelegate _next;

        public MiddlewareErroresGlobal(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ResponseModel<string>
                {
                    Exito = false,
                    Mensaje = "Ocurrió un error interno en el servidor.",
                    Data = ex.Message
                };

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
