using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddOwnDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
            services.AddScoped<ICalculatorService, CalculatorService>();
            return services;
		}
	}
}