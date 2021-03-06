using System;
using TaxCalculator.Models.Base;

namespace TaxCalculator.Models.Entities
{
    public class TaxPayerContract: BaseModel
    {
        public string FullName { get; set; }

        public long SSN { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public double GrossIncome { get; set; }

        public double? CharitySpent { get; set; }

        public double? IncomeTax { get; set; }

        public double? SocialTax { get; set; }

        public double? TotalTax { get; set; }

        public double NetIncome { get; set; }
    }
}
