using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Models.RequestModels
{
    class TaxPayerContractModel
    {
        public string FullName { get; set; }

        public int SSN { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public double GrossIncome { get; set; }

        public double? CharitySpent { get; set; }
    }
}
