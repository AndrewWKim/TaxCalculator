using TaxCalculator.Models.Entities;
using TaxCalculator.Models.RequestModels;

namespace TaxCalculator.Services.Interfaces
{
    public interface ICalculatorService
    {
        TaxPayerContract CalculateTaxes(TaxPayerContractModel contract);
    }
}
