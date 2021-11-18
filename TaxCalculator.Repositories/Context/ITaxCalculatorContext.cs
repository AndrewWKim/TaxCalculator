using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Models.Entities;

namespace TaxCalculator.Repositories.Context
{
    public interface ITaxCalculatorContext
    {
        DbSet<TaxPayerContract> TaxPayerContracts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
