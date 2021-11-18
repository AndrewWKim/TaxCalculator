using System.Threading.Tasks;
using TaxCalculator.Models.Entities;
using TaxCalculator.Models.RequestModels;

namespace TaxCalculator.Services.Interfaces
{
    public interface ICalculatorService
    {
       Task<TaxPayerContract> CalculateTaxesAsync(TaxPayerContractModel contract);
    }
}
