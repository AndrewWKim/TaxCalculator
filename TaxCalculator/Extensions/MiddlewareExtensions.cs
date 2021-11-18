using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using TaxCalculator.Middlewares;

namespace TaxCalculator.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGeneralExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GeneralExceptionMiddleware>();
        }
    }
}
