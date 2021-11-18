using System.Threading.Tasks;
using TaxCalculator.Models.Base;
using TaxCalculator.Repositories.Context;
using TaxCalculator.Repositories.Interfaces;

namespace TaxCalculator.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseModel
    {
        public BaseRepository(ITaxCalculatorContext context)
        {
            TaxCalculatorContext = context;
        }

        public ITaxCalculatorContext TaxCalculatorContext { get; set; }

        public abstract Task<int> CreateAsync(TEntity entity);
    }
}
