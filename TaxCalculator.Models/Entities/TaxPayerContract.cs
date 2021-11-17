using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models.Base;

namespace TaxCalculator.Models.Entities
{
    public class TaxPayerContract: BaseModel
    {
        public string FullName { get; set; }

        public int SSN { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public double GrossIncome { get; set; }

        public double NetIncome { get; set; }

        public double? CharitySpent { get; set; }

        public double? SocialTax { get; set; }

        public double? TotalTax { get; set; }

        public double? IncomeTax { get; set; }
    }
}
