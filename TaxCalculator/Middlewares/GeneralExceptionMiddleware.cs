using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TaxCalculator.Models.Exceptions;

namespace TaxCalculator.Middlewares
{
    public class GeneralExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GeneralExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                var modelState = new ModelStateDictionary();

                foreach (var error in ex.Errors)
                {
                    modelState.AddModelError(error.FieldName, error.Message);
                }

                await HandleException(context.Response, ex, HttpStatusCode.UnprocessableEntity, new SerializableError(modelState));
            }
            catch (Exception ex)
            {
                await HandleException(context.Response, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private Task HandleException(HttpResponse response, Exception exception, HttpStatusCode statusCode, object responseToWrite = null)
        {
            response.StatusCode = (int)statusCode;
            response.Headers.Add("X-Status-Reason", "Validation error");
            response.Headers.Add("Content-Type", "application/json");

            return responseToWrite != null ?
                response.WriteAsync(
                    JsonConvert.SerializeObject(
                        responseToWrite,
                        Formatting.None,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }))
                : Task.CompletedTask;
        }
    }
}
