using System;
using TaxCalculator.Models.Entities;
using TaxCalculator.Models.RequestModels;

namespace TaxCalculator.UnitTests.TestData
{
    public static class TestTaxPayerContract
    {
        public static (TaxPayerContractModel, TaxPayerContract) GetContractLessMin()
        {
            var contractLessMin = new TaxPayerContractModel()
            {
                FullName = "George Smith",
                SSN = 1234567,
                DateOfBirth = DateTime.Now.AddYears(-30),
                GrossIncome = 980
            };

            var correctResult = new TaxPayerContract()
            {
                GrossIncome = 980,
                CharitySpent = null,
                IncomeTax = null,
                SocialTax = null,
                TotalTax = null,
                NetIncome = 980
            };

            return (contractLessMin, correctResult);
        }

        public static (TaxPayerContractModel, TaxPayerContract) GetContractHigherMaxNoCharity()
        {
            var contractLessMin = new TaxPayerContractModel()
            {
                FullName = "Irina Smith",
                SSN = 12345678,
                DateOfBirth = DateTime.Now.AddYears(-35),
                GrossIncome = 3400
            };

            var correctResult = new TaxPayerContract()
            {
                GrossIncome = 3400,
                CharitySpent = null,
                IncomeTax = 240,
                SocialTax = 300,
                TotalTax = 540,
                NetIncome = 2860
            };

            return (contractLessMin, correctResult);
        }

        public static (TaxPayerContractModel, TaxPayerContract) GetContractHigherMaxWithCharity()
        {
            var contractLessMin = new TaxPayerContractModel()
            {
                FullName = "Bill Smith",
                SSN = 7777777777,
                DateOfBirth = DateTime.Now.AddYears(-27),
                GrossIncome = 3600,
                CharitySpent = 520
            };

            var correctResult = new TaxPayerContract()
            {
                GrossIncome = 3400,
                CharitySpent = 520,
                IncomeTax = 224,
                SocialTax = 300,
                TotalTax = 524,
                NetIncome = 3076
            };

            return (contractLessMin, correctResult);
        }

        public static (TaxPayerContractModel, TaxPayerContract) GetContractWithCharity()
        {
            var contractLessMin = new TaxPayerContractModel()
            {
                FullName = "Mick Smith",
                SSN = 7689453,
                DateOfBirth = DateTime.Now.AddYears(-47),
                GrossIncome = 2500,
                CharitySpent = 150
            };

            var correctResult = new TaxPayerContract()
            {
                GrossIncome = 3400,
                CharitySpent = 150,
                IncomeTax = 135,
                SocialTax = 202.5,
                TotalTax = 337.5,
                NetIncome = 2162.5
            };

            return (contractLessMin, correctResult);
        }
    }
}
