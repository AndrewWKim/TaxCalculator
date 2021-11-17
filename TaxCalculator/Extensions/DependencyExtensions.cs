using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;

namespace CottageApi.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddOwnDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICalculatorService, CalculatorService>();
            return services;
		}
	}
}