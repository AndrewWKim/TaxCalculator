using System;

namespace TaxCalculator.Models.RequestModels
{
    public class TaxPayerContractModel
    {
        public string FullName { get; set; }

        public int SSN { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public double GrossIncome { get; set; }

        public double? CharitySpent { get; set; }
    }
}
