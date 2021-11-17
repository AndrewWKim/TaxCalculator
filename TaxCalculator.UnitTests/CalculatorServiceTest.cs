using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.UnitTests.Base;
using TaxCalculator.UnitTests.TestData;

namespace TaxCalculator.UnitTests
{
    [TestClass]
    public class CalculatorServiceTest : BaseTest<ICalculatorService>
    {
        [TestInitialize]
        public void Initialize()
        {
            BaseInit();
            var taxCalculatorService = new TaxCalculatorService();
            TestedInstance = new CalculatorService(Mapper, taxCalculatorService);
        }

        [TestMethod]
        public void CalculateContractLessMin_Sucess()
        {
            var contractsTuple = TestTaxPayerContract.GetContractLessMin();
            var validResult = contractsTuple.Item2;

            var contract = TestedInstance.CalculateTaxes(contractsTuple.Item1);

            Assert.AreEqual(contract.GrossIncome, validResult.GrossIncome);
            Assert.AreEqual(contract.NetIncome, validResult.NetIncome);
            Assert.IsNull(contract.CharitySpent);
            Assert.IsNull(contract.IncomeTax);
            Assert.IsNull(contract.SocialTax);
            Assert.IsNull(contract.TotalTax);
        }
    }
}