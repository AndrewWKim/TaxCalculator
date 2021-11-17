using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController: ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost]
        public async Task<object> Calculate()
        {
            return 123;
        }
    }
}
