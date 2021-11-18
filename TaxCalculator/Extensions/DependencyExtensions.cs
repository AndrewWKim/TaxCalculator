using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Repositories;
using TaxCalculator.Repositories.Context;
using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddOwnDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITaxCalculatorContext>(provider => provider.GetService<TaxCalculatorContext>());
            services.AddScoped<ITaxPayerContractRepository, TaxPayerContractRepository>();
            services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
            services.AddScoped<ICalculatorService, CalculatorService>();
            return services;
		}
	}
}