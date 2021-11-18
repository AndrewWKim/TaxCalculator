using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TaxCalculator.Models.Configurations;
using TaxCalculator.Models.Entities;
using TaxCalculator.Repositories.Context;
using TaxCalculator.Repositories.Interfaces;

namespace TaxCalculator.Repositories
{
    public class TaxPayerContractRepository : BaseRepository<TaxPayerContract>, ITaxPayerContractRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly int _casheMinutes;

        public TaxPayerContractRepository(
            ITaxCalculatorContext context,
            IMemoryCache memoryCache,
            Config config) : base(context)
        {
            _memoryCache = memoryCache;
            _casheMinutes = config.CacheMinutes;
        }

        public override async Task<int> CreateAsync(TaxPayerContract contract)
        {
            var lastId = GetLastId();
            var newContract = new TaxPayerContract
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
            };

            TaxCalculatorContext.TaxPayerContracts.Add(newContract);

            await TaxCalculatorContext.SaveChangesAsync();

            _memoryCache.Set(CreateCacheContractKey(
                    newContract.SSN, newContract.GrossIncome, newContract.CharitySpent),
                    newContract,
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_casheMinutes)
                    });

            return lastId;
        }

        public async Task<TaxPayerContract> GetAlreadyCalculatedAsync(long SSN, double grossIncome, double? charitySpent)
        {
            TaxPayerContract contract = null;

            var contractCacheKey = CreateCacheContractKey(SSN, grossIncome, charitySpent);

            if (!_memoryCache.TryGetValue(contractCacheKey, out contract))
            {
                contract = await TaxCalculatorContext.TaxPayerContracts.FirstOrDefaultAsync(c =>
                    c.SSN == SSN
                    && c.GrossIncome == grossIncome
                    && c.CharitySpent == charitySpent
                );

                if (contract != null)
                {
                    _memoryCache.Set(contractCacheKey, contract,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_casheMinutes)));
                }
            }
            return contract;
        }

        private int GetLastId()
        {
            if (TaxCalculatorContext.TaxPayerContracts.Any())
            {
                return TaxCalculatorContext.TaxPayerContracts.Max(_ => _.Id);
            }

            return 0;
        }

        private string CreateCacheContractKey(long SSN, double grossIncome, double? charitySpent)
        {
            return $"{SSN},{grossIncome},{charitySpent}";
        }
    }
}
