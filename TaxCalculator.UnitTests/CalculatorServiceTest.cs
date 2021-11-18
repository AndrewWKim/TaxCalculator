using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Models.Exceptions;
using TaxCalculator.Models.RequestModels;
using TaxCalculator.Repositories;
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
            var taxPayerContractRepository = new TaxPayerContractRepository(TaxCalculatorContext, MemoryCache, Config);
            TestedInstance = new CalculatorService(Mapper, taxCalculatorService, taxPayerContractRepository);
        }

        [TestMethod]
        public async Task CalculateContractLessMin_Sucess()
        {
            var contractsTuple = TestTaxPayerContract.GetContractLessMin();
            var validResult = contractsTuple.Item2;

            var contract = await TestedInstance.CalculateTaxesAsync(contractsTuple.Item1);

            Assert.AreEqual(contract.GrossIncome, validResult.GrossIncome);
            Assert.AreEqual(contract.NetIncome, validResult.NetIncome);
            Assert.IsNull(contract.CharitySpent);
            Assert.IsNull(contract.IncomeTax);
            Assert.IsNull(contract.SocialTax);
            Assert.IsNull(contract.TotalTax);
        }

        [TestMethod]
        public async Task CalculateContractHigherMaxNoCharity_Sucess()
        {
            var contractsTuple = TestTaxPayerContract.GetContractHigherMaxNoCharity();
            var validResult = contractsTuple.Item2;

            var contract = await TestedInstance.CalculateTaxesAsync(contractsTuple.Item1);

            Assert.AreEqual(contract.GrossIncome, validResult.GrossIncome);
            Assert.AreEqual(contract.NetIncome, validResult.NetIncome);
            Assert.IsNull(contract.CharitySpent);
            Assert.AreEqual(contract.IncomeTax, validResult.IncomeTax);
            Assert.AreEqual(contract.SocialTax, validResult.SocialTax);
            Assert.AreEqual(contract.TotalTax, validResult.TotalTax);
        }

        [TestMethod]
        public async Task CalculateContractHigherMaxWithCharity_Sucess()
        {
            var contractsTuple = TestTaxPayerContract.GetContractHigherMaxWithCharity();
            var validResult = contractsTuple.Item2;

            var contract = await TestedInstance.CalculateTaxesAsync(contractsTuple.Item1);

            Assert.AreEqual(contract.GrossIncome, validResult.GrossIncome);
            Assert.AreEqual(contract.NetIncome, validResult.NetIncome);
            Assert.AreEqual(contract.CharitySpent, validResult.CharitySpent);
            Assert.AreEqual(contract.IncomeTax, validResult.IncomeTax);
            Assert.AreEqual(contract.SocialTax, validResult.SocialTax);
            Assert.AreEqual(contract.TotalTax, validResult.TotalTax);
        }

        [TestMethod]
        public async Task CalculateContractWithCharity_Sucess()
        {
            var contractsTuple = TestTaxPayerContract.GetContractWithCharity();
            var validResult = contractsTuple.Item2;

            var contract = await TestedInstance.CalculateTaxesAsync(contractsTuple.Item1);

            Assert.AreEqual(contract.GrossIncome, validResult.GrossIncome);
            Assert.AreEqual(contract.NetIncome, validResult.NetIncome);
            Assert.AreEqual(contract.CharitySpent, validResult.CharitySpent);
            Assert.AreEqual(contract.IncomeTax, validResult.IncomeTax);
            Assert.AreEqual(contract.SocialTax, validResult.SocialTax);
            Assert.AreEqual(contract.TotalTax, validResult.TotalTax);
        }

        [TestMethod]
        public async Task CalculateContractInvalidName_ThrowValidationException()
        {
            var contract = TestTaxPayerContractInvalid.GetContractInvalidName();

            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => TestedInstance.CalculateTaxesAsync(contract));

            Assert.IsTrue(exception.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.FullName)));
        }

        [TestMethod]
        public async Task CalculateContractSSNLength_ThrowValidationException()
        {
            var contractShortSSN = TestTaxPayerContractInvalid.GetContractShortSSN();
            var contractLongSSN = TestTaxPayerContractInvalid.GetContractLongSSN();

            var exceptionShort = await Assert.ThrowsExceptionAsync<ValidationException>(() => TestedInstance.CalculateTaxesAsync(contractShortSSN));
            var exceptionLong = await Assert.ThrowsExceptionAsync<ValidationException>(() => TestedInstance.CalculateTaxesAsync(contractLongSSN));

            Assert.IsTrue(exceptionShort.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.SSN)));
            Assert.IsTrue(exceptionLong.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.SSN)));
        }

        [TestMethod]
        public async Task CalculateContractEmptySSN_ThrowValidationException()
        {
            var contract = TestTaxPayerContractInvalid.GetContractEmptySSN();

            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => TestedInstance.CalculateTaxesAsync(contract));

            Assert.IsTrue(exception.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.SSN)));
        }

        [TestMethod]
        public async Task CalculateContractGrossIncome_ThrowValidationException()
        {
            var contract = TestTaxPayerContractInvalid.GetContractInvalidGross();

            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => TestedInstance.CalculateTaxesAsync(contract));

            Assert.IsTrue(exception.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.GrossIncome)));
        }

        [TestMethod]
        public async Task CalculateContractCharitySpent_ThrowValidationException()
        {
            var contract = TestTaxPayerContractInvalid.GetContractInvalidCharity();

            var exception = await Assert.ThrowsExceptionAsync<ValidationException>(() => TestedInstance.CalculateTaxesAsync(contract));

            Assert.IsTrue(exception.Errors.Any(err => err.FieldName == nameof(TaxPayerContractModel.CharitySpent)));
        }
    }
}
