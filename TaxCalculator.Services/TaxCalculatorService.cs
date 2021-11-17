using System;
using TaxCalculator.Models.Entities;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services
{
    public class TaxCalculatorService: ITaxCalculatorService
    {
        public TaxPayerContract CalculateAllTaxes(TaxPayerContract contract)
        {
            if (contract.GrossIncome <= 1000)
            {
                contract.NetIncome = contract.GrossIncome;
                return contract;
            }

            var taxableValue = CalculateTaxableValueFromCharity(contract.GrossIncome, contract.CharitySpent);

            contract.IncomeTax = CalculateIncomeTax(taxableValue);
            contract.SocialTax = CalculateSocialTax(taxableValue);
            contract.TotalTax = CalculateTotalTax(contract);
            contract.NetIncome = RoundToZero(contract.GrossIncome - contract.TotalTax.Value);

            return contract;
        }

        private double CalculateTaxableValueFromCharity(double grossIncome, double? charitySpent)
        {
            if (charitySpent.HasValue)
            {
                var maxCharityValue = grossIncome * 0.1;
                grossIncome = charitySpent.Value > maxCharityValue 
                    ? grossIncome - maxCharityValue 
                    : grossIncome - charitySpent.Value;
            }

            return Math.Round(grossIncome, 2, MidpointRounding.ToZero);
        }

        private double CalculateIncomeTax(double income)
        {
            if (income < 1000)
            {
                return 0;
            }

            return Math.Round((income - 1000) * 0.1, 2, MidpointRounding.ToZero);
        }

        private double CalculateSocialTax(double income)
        {
            double result = 0;
            if (income < 1000)
            {
                return result;
            }

            if (income > 3000)
            {
                result = (3000 - 1000) * 0.15;
            }
            else
            {
                result = (income - 1000) * 0.15;
            }

            return RoundToZero(result);
        }

        private double CalculateTotalTax(TaxPayerContract contract)
        {
            var totalTaxes = contract.IncomeTax.Value + contract.SocialTax.Value;

            return RoundToZero(totalTaxes);
        }

        private double RoundToZero(double value, int? digits = 2)
        {
            return Math.Round(value, digits.Value, MidpointRounding.ToZero);
        }
    }
}
