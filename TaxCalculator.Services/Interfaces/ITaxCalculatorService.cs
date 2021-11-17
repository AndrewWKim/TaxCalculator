using TaxCalculator.Models.Entities;

namespace TaxCalculator.Services.Interfaces
{
    public interface ITaxCalculatorService
    {
        TaxPayerContract CalculateAllTaxes(TaxPayerContract contract);
    }
}
