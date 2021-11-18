using System.IO;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Mappings;
using TaxCalculator.Models.Configurations;
using TaxCalculator.Repositories.Context;

namespace TaxCalculator.UnitTests.Base
{
    public abstract class BaseTest<TTestedInstance>
        where TTestedInstance : class
    {
        protected IMapper Mapper;
        protected TTestedInstance TestedInstance;
        protected Config Config;
        protected ITaxCalculatorContext TaxCalculatorContext;
        protected IMemoryCache MemoryCache;

        protected void BaseInit()
        {
            var mappings = new MapperConfigurationExpression();
            mappings.AddProfile<MappingProfile>();
            Mapper = new Mapper(new MapperConfiguration(mappings));

            var options = new DbContextOptionsBuilder<TaxCalculatorContext>()
                .UseInMemoryDatabase(databaseName: "TaxCalculator")
                .Options;
            TaxCalculatorContext = new TaxCalculatorContext(options);

            InitConfig();
            InitCache();
        }

        private void InitConfig()
        {
            var config = new Config();
            var configFile = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            configFile.GetSection("TaxCalculatorAPI").Bind(config); ;
            Config = config;
        }

        private void InitCache()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            MemoryCache = serviceProvider.GetService<IMemoryCache>();
        }
    }
}
