using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models.RequestModels;
using TaxCalculator.Models.ViewModels;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        private readonly IMapper _mapper;

        public CalculatorController(
            ICalculatorService calculatorService,
            IMapper mapper)
        {
            _mapper = mapper;
            _calculatorService = calculatorService;
        }

        [HttpPost]
        public async Task<TaxPayerContractViewModel> Calculate([FromBody]TaxPayerContractModel contract)
        {
            var taxPayerContract = _calculatorService.CalculateTaxes(contract);
            var contractTaxResult = _mapper.Map<TaxPayerContractViewModel>(taxPayerContract);
            return contractTaxResult;
        }
    }
}
