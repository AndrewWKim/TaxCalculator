using System.Text.RegularExpressions;
using TaxCalculator.Models.Exceptions;
using TaxCalculator.Models.RequestModels;

namespace TaxCalculator.Core.Utils
{
    public static class ModelValidation
    {
        public static void ValidateContract(TaxPayerContractModel contract)
        {
            if (contract.GrossIncome < 0)
            {
                throw new ValidationException(nameof(TaxPayerContractModel.GrossIncome), "Gross Income value can't be negative");
            }

            if (contract.CharitySpent.HasValue && contract.CharitySpent.Value < 0)
            {
                throw new ValidationException(nameof(TaxPayerContractModel.CharitySpent), "Charity Spent value can't be negative");
            }

            if (contract.SSN == 0)
            {
                throw new ValidationException(nameof(TaxPayerContractModel.SSN), "SSN is required");
            }

            var ssnLength = contract.SSN.ToString().Length;

            if (ssnLength < 5 || ssnLength > 10)
            {
                throw new ValidationException(nameof(TaxPayerContractModel.SSN), "SSN length should be from 5 to 10 digits");
            }

            if (!IsFullNameValid(contract.FullName))
            {
                throw new ValidationException(nameof(TaxPayerContractModel.FullName), "Full Name at least two words(only symbols) separated by space");
            }
        }

        private static bool IsFullNameValid(string fullName)
        {
            Regex regex = new Regex("^[A-Z][a-z]*(\\s[A-Z][a-z]*)+$");
            return regex.IsMatch(fullName);
        }
    }
}
