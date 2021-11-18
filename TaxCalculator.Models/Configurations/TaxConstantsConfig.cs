using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Models.Configurations
{
    public class TaxConstantsConfig
    {
        private double _incomeTaxPercent;

        private double _socialTaxPercent;

        private double _maxCharitySpentPercent;

        public int MinTaxationValue { get; set; }

        public double IncomeTaxPercent
        {
            get { return _incomeTaxPercent; }
            set { _incomeTaxPercent = value / 100; }
        }

        public int MinSocialTax { get; set; }

        public int MaxSocialTax { get; set; }

        public double SocialTaxPercent
        {
            get { return _socialTaxPercent; }
            set { _socialTaxPercent = value / 100; }
        }

        public double MaxCharitySpentPercent
        {
            get { return _maxCharitySpentPercent;}
            set { _maxCharitySpentPercent = value / 100; }
        }
    }
}
