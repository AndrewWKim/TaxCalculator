namespace TaxCalculator.Models.ViewModels
{
    public class TaxPayerContractViewModel
    {

        public double GrossIncome { get; set; }

        public double NetIncome { get; set; }

        public double CharitySpent { get; set; }

        public double SocialTax { get; set; }

        public double TotalTax { get; set; }

        public double IncomeTax { get; set; }
    }
}
