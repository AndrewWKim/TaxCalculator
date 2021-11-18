using System;
using TaxCalculator.Models.Configurations;
using TaxCalculator.Models.Entities;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services
{
    public class TaxCalculatorService: ITaxCalculatorService
    {
        private readonly TaxConstantsConfig _taxConstantsConfig;

        public TaxCalculatorService(Config config)
        {
            _taxConstantsConfig = config.TaxConstants;
        }

        public TaxPayerContract CalculateAllTaxes(TaxPayerContract contract)
        {
            if (contract.GrossIncome <= _taxConstantsConfig.MinTaxationValue)
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
                var maxCharityValue = grossIncome * _taxConstantsConfig.MaxCharitySpentPercent;
                grossIncome = charitySpent.Value > maxCharityValue 
                    ? grossIncome - maxCharityValue 
                    : grossIncome - charitySpent.Value;
            }

            return RoundToZero(grossIncome);
        }

        private double CalculateIncomeTax(double income)
        {
            if (income < _taxConstantsConfig.MinTaxationValue)
            {
                return 0;
            }

            return RoundToZero((income - _taxConstantsConfig.MinTaxationValue) * _taxConstantsConfig.IncomeTaxPercent);
        }

        private double CalculateSocialTax(double income)
        {
            double result = 0;
            if (income < _taxConstantsConfig.MinSocialTax)
            {
                return result;
            }

            if (income > _taxConstantsConfig.MaxSocialTax)
            {
                result = (_taxConstantsConfig.MaxSocialTax - _taxConstantsConfig.MinSocialTax) * _taxConstantsConfig.SocialTaxPercent;
            }
            else
            {
                result = (income - _taxConstantsConfig.MinSocialTax) * _taxConstantsConfig.SocialTaxPercent;
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
