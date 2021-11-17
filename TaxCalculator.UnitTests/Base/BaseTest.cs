
using AutoMapper;
using AutoMapper.Configuration;
using TaxCalculator.Mappings;

namespace TaxCalculator.UnitTests.Base
{
    public abstract class BaseTest<TTestedInstance>
        where TTestedInstance : class
    {
        protected IMapper Mapper;
        protected TTestedInstance TestedInstance;

        protected void BaseInit()
        {
            var mappings = new MapperConfigurationExpression();
            mappings.AddProfile<MappingProfile>();
            Mapper = new Mapper(new MapperConfiguration(mappings));
        }
    }
}
