using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Models.Entities;
using TaxCalculator.Repositories.Context;
using TaxCalculator.Repositories.Interfaces;

namespace TaxCalculator.Repositories
{
    public class TaxPayerContractRepository : BaseRepository<TaxPayerContract>, ITaxPayerContractRepository
    {
        public TaxPayerContractRepository(ITaxCalculatorContext context) : base(context)
        {
        }

        public override async Task<int> CreateAsync(TaxPayerContract contract)
        {
            var lastId = GetLastId();
            TaxCalculatorContext.TaxPayerContracts.Add(new TaxPayerContract
            {
                Id = ++lastId,
                FullName = contract.FullName,
                SSN = contract.SSN,
                DateOfBirth = contract.DateOfBirth,
                GrossIncome = contract.GrossIncome,
                CharitySpent = contract.CharitySpent,
                IncomeTax = contract.IncomeTax,
                SocialTax = contract.SocialTax,
                TotalTax = contract.TotalTax,
                NetIncome = contract.NetIncome,
            });

            await TaxCalculatorContext.SaveChangesAsync();

            return lastId;
        }

        public async Task<TaxPayerContract> GetAlreadyCalculatedAsync(long SSN, double grossIncome, double? charitySpent)
        {
            var contract = await TaxCalculatorContext.TaxPayerContracts.FirstOrDefaultAsync(c =>
                c.SSN == SSN
                && c.GrossIncome == grossIncome
                && c.CharitySpent == charitySpent
            );

            return contract;
        }

        private int GetLastId()
        {
            if (TaxCalculatorContext.TaxPayerContracts.Count() > 0)
            {
                return TaxCalculatorContext.TaxPayerContracts.Max(_ => _.Id);
            }

            return 0;
        }
    }
}
