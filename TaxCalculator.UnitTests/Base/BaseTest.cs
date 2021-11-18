using System.IO;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TaxCalculator.Mappings;
using TaxCalculator.Models.Configurations;

namespace TaxCalculator.UnitTests.Base
{
    public abstract class BaseTest<TTestedInstance>
        where TTestedInstance : class
    {
        protected IMapper Mapper;
        protected TTestedInstance TestedInstance;
        protected Config Config;

        protected void BaseInit()
        {
            var mappings = new MapperConfigurationExpression();
            mappings.AddProfile<MappingProfile>();
            Mapper = new Mapper(new MapperConfiguration(mappings));
            InitConfig();
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
    }
}
