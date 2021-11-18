using System.Threading.Tasks;
using TaxCalculator.Models.Base;
using TaxCalculator.Repositories.Context;

namespace TaxCalculator.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseModel
    {
        ITaxCalculatorContext TaxCalculatorContext { get; set; }

        Task<int> CreateAsync(TEntity entity);
    }
}
