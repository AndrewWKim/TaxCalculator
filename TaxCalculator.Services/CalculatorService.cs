using AutoMapper;
using TaxCalculator.Models.Entities;
using TaxCalculator.Models.Exceptions;
using TaxCalculator.Models.RequestModels;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Services.Utils;

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
            ModelValidation.ValidateContract(contract);

            var taxPayerContract = _mapper.Map<TaxPayerContract>(contract);

            taxPayerContract = _taxCalculatorService.CalculateAllTaxes(taxPayerContract);

            // TODO add saving contract here

            return taxPayerContract;
        }
    }
}
