using Microsoft.EntityFrameworkCore;
using TaxCalculator.Models.Entities;

namespace TaxCalculator.Repositories.Context
{
    public class TaxCalculatorContext : DbContext, ITaxCalculatorContext
    {
        public DbSet<TaxPayerContract> TaxPayerContracts { get; set; }

        public TaxCalculatorContext(DbContextOptions<TaxCalculatorContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TaxCalculator");
        }
    }
}
