using AutoMapper;
using TaxCalculator.Models.Entities;
using TaxCalculator.Models.RequestModels;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services
{
    public class CalculatorService: ICalculatorService
    {
        private readonly IMapper _mapper;
        private readonly ITaxCalculatorService _taxCalculatorService;

        public CalculatorService(IMapper mapper, ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService;
            _mapper = mapper;
        }

        public TaxPayerContract CalculateTaxes(TaxPayerContractModel contract)
        {
            // TODO add validation here
            var taxPayerContract = _mapper.Map<TaxPayerContract>(contract);

            taxPayerContract = _taxCalculatorService.CalculateAllTaxes(taxPayerContract);

            // TODO add saving contract here

            return taxPayerContract;
        }
    }
}
