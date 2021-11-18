using System.Threading.Tasks;
using TaxCalculator.Models.Entities;

namespace TaxCalculator.Repositories.Interfaces
{
    public interface ITaxPayerContractRepository : IBaseRepository<TaxPayerContract>
    {
        Task<TaxPayerContract> GetAlreadyCalculatedAsync(long SSN, double grossIncome, double? charitySpent);
    }
}
