using System;
using TaxCalculator.Models.RequestModels;

namespace TaxCalculator.UnitTests.TestData
{
    public static class TestTaxPayerContractInvalid
    {
        public static TaxPayerContractModel GetContractInvalidName()
        {
            return new TaxPayerContractModel()
            {
                FullName = "Mick456Smith",
                SSN = 7689453,
                DateOfBirth = DateTime.Now.AddYears(-100),
                GrossIncome = 8888
            };
        }

        public static TaxPayerContractModel GetContractShortSSN()
        {
            return new TaxPayerContractModel()
            {
                FullName = "Mick Smith",
                SSN = 123,
                DateOfBirth = DateTime.Now.AddYears(-100),
                GrossIncome = 8888
            };
        }

        public static TaxPayerContractModel GetContractLongSSN()
        {
            return new TaxPayerContractModel()
            {
                FullName = "Mick Smith",
                SSN = 1234567891234,
                DateOfBirth = DateTime.Now.AddYears(-100),
                GrossIncome = 8888
            };
        }

        public static TaxPayerContractModel GetContractInvalidGross()
        {
            return new TaxPayerContractModel()
            {
                FullName = "Mick Smith",
                SSN = 1234567,
                DateOfBirth = DateTime.Now.AddYears(-100),
                GrossIncome = -1067
            };
        }

        public static TaxPayerContractModel GetContractInvalidCharity()
        {
            return new TaxPayerContractModel()
            {
                FullName = "Mick Smith",
                SSN = 1234567,
                DateOfBirth = DateTime.Now.AddYears(-100),
                GrossIncome = 1067,
                CharitySpent = -5678
            };
        }
    }
}
