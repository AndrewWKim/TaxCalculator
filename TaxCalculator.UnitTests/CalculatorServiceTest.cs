using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaxCalculator.Models.Exceptions;
using TaxCalculator.Models.RequestModels;
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
            var taxCalculatorService = new TaxCalculatorService(Config);
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

        [TestMethod]
        public void CalculateContractHigherMaxNoCharity_Sucess()
        {
            var contractsTuple = TestTaxPayerContract.GetContractHigherMaxNoCharity();
            var validResult = contractsTuple.Item2;

            var contract = TestedInstance.CalculateTaxes(contractsTuple.Item1);

            Assert.AreEqual(contract.GrossIncome, validResult.GrossIncome);
            Assert.AreEqual(contract.NetIncome, validResult.NetIncome);
            Assert.IsNull(contract.CharitySpent);
            Assert.AreEqual(contract.IncomeTax, validResult.IncomeTax);
            Assert.AreEqual(contract.SocialTax, validResult.SocialTax);
            Assert.AreEqual(contract.TotalTax, validResult.TotalTax);
        }

        [TestMethod]
        public void CalculateContractHigherMaxWithCharity_Sucess()
        {
            var contractsTuple = TestTaxPayerContract.GetContractHigherMaxWithCharity();
            var validResult = contractsTuple.Item2;

            var contract = TestedInstance.CalculateTaxes(contractsTuple.Item1);

            Assert.AreEqual(contract.GrossIncome, validResult.GrossIncome);
            Assert.AreEqual(contract.NetIncome, validResult.NetIncome);
            Assert.AreEqual(contract.CharitySpent, validResult.CharitySpent);
            Assert.AreEqual(contract.IncomeTax, validResult.IncomeTax);
            Assert.AreEqual(contract.SocialTax, validResult.SocialTax);
            Assert.AreEqual(contract.TotalTax, validResult.TotalTax);
        }

        [TestMethod]
        public void CalculateContractWithCharity_Sucess()
        {
            var contractsTuple = TestTaxPayerContract.GetContractWithCharity();
            var validResult = contractsTuple.Item2;

            var contract = TestedInstance.CalculateTaxes(contractsTuple.Item1);

            Assert.AreEqual(contract.GrossIncome, validResult.GrossIncome);
            Assert.AreEqual(contract.NetIncome, validResult.NetIncome);
            Assert.AreEqual(contract.CharitySpent, validResult.CharitySpent);
            Assert.AreEqual(contract.IncomeTax, validResult.IncomeTax);
            Assert.AreEqual(contract.SocialTax, validResult.SocialTax);
            Assert.AreEqual(contract.TotalTax, validResult.TotalTax);
        }

        [TestMethod]
        public void CalculateContractInvalidName_ThrowValidationException()
        {
            var contract = TestTaxPayerContractInvalid.GetContractInvalidName();

            var exception = Assert.ThrowsException<ValidationException>(() => TestedInstance.CalculateTaxes(contract));

            Assert.IsTrue(exception.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.FullName)));
        }

        [TestMethod]
        public void CalculateContractSSNLength_ThrowValidationException()
        {
            var contractShortSSN = TestTaxPayerContractInvalid.GetContractShortSSN();
            var contractLongSSN = TestTaxPayerContractInvalid.GetContractLongSSN();

            var exceptionShort = Assert.ThrowsException<ValidationException>(() => TestedInstance.CalculateTaxes(contractShortSSN));
            var exceptionLong = Assert.ThrowsException<ValidationException>(() => TestedInstance.CalculateTaxes(contractLongSSN));

            Assert.IsTrue(exceptionShort.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.SSN)));
            Assert.IsTrue(exceptionLong.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.SSN)));
        }

        [TestMethod]
        public void CalculateContractEmptySSN_ThrowValidationException()
        {
            var contract = TestTaxPayerContractInvalid.GetContractEmptySSN();

            var exception = Assert.ThrowsException<ValidationException>(() => TestedInstance.CalculateTaxes(contract));

            Assert.IsTrue(exception.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.SSN)));
        }

        [TestMethod]
        public void CalculateContractGrossIncome_ThrowValidationException()
        {
            var contract = TestTaxPayerContractInvalid.GetContractInvalidGross();

            var exception = Assert.ThrowsException<ValidationException>(() => TestedInstance.CalculateTaxes(contract));

            Assert.IsTrue(exception.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.GrossIncome)));
        }

        [TestMethod]
        public void CalculateContractCharitySpent_ThrowValidationException()
        {
            var contract = TestTaxPayerContractInvalid.GetContractInvalidCharity();

            var exception = Assert.ThrowsException<ValidationException>(() => TestedInstance.CalculateTaxes(contract));

            Assert.IsTrue(exception.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.CharitySpent)));
        }
    }
}
