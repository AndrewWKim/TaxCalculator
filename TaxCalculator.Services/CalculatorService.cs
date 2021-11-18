using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using TaxCalculator.Models.Entities;
using TaxCalculator.Models.RequestModels;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Core.Utils;
using TaxCalculator.Repositories.Interfaces;

namespace TaxCalculator.Services
{
    public class CalculatorService: ICalculatorService
    {
        private readonly IMapper _mapper;
        private readonly ITaxCalculatorService _taxCalculatorService;
        private readonly ITaxPayerContractRepository _taxPayerContractRepository;

        public CalculatorService(
            IMapper mapper, 
            ITaxCalculatorService taxCalculatorService,
            ITaxPayerContractRepository taxPayerContractRepository)
        {
            _taxCalculatorService = taxCalculatorService;
            _mapper = mapper;
            _taxPayerContractRepository = taxPayerContractRepository;
        }

        public async Task<TaxPayerContract> CalculateTaxesAsync(TaxPayerContractModel contract)
        {
            ModelValidation.ValidateContract(contract);

            var alreadyCalculatedContract =
                await _taxPayerContractRepository.GetAlreadyCalculatedAsync(contract.SSN, contract.GrossIncome,
                    contract.CharitySpent);

            if (alreadyCalculatedContract != null)
            {
                return alreadyCalculatedContract;
            }

            var taxPayerContract = _mapper.Map<TaxPayerContract>(contract);

            taxPayerContract = _taxCalculatorService.CalculateAllTaxes(taxPayerContract);

            await _taxPayerContractRepository.CreateAsync(taxPayerContract);

            return taxPayerContract;
        }
    }
}
