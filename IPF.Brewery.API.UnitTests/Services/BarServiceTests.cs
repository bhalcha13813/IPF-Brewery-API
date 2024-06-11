using System.Net;
using FakeItEasy;
using FluentValidation.Results;
using IPF.Brewery.API.Services;
using IPF.Brewery.API.Validation;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;

namespace IPF.Brewery.API.UnitTests.Services
{
    [TestFixture()]
    public class BarServiceTests
    {
        private IAddBarValidator fakeAddBarValidator;
        private IBarRepository fakeBarRepository;
        private IBeerRepository fakeBeerRepository;
        private IBarService barService;

        [SetUp]
        public void Setup()
        {
            fakeAddBarValidator = A.Fake<IAddBarValidator>();
            fakeBarRepository = A.Fake<IBarRepository>();
            fakeBeerRepository = A.Fake<IBeerRepository>();
            barService = new BarService(fakeAddBarValidator, fakeBarRepository, fakeBeerRepository);
        }

        [Test]
        public void Test_validateAddBar_Returns_Error_When_InvalidPayload()
        {
            var validationMessage = new ValidationFailure("prop1", "error message");
            validationMessage.ErrorCode = HttpStatusCode.BadRequest.ToString();

            A.CallTo(() => fakeAddBarValidator.Validate(A<BarPayload>.Ignored))
                            .Returns(new ValidationResult(new List<ValidationFailure> { validationMessage }));

            var result =  barService.validateAddBar(new BarPayload());
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("error message", result.Errors.Single().ErrorMessage);
        }

        [Test] 
        public void Test_validateAddBar_Returns_Success_When_ValidPayload()
        {
            A.CallTo(() => fakeAddBarValidator.Validate(A<BarPayload>.Ignored))
                 .Returns(new ValidationResult(new List<ValidationFailure>()));

            var result = barService.validateAddBar(new BarPayload());
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Test_GetBars_Returns_Bars()
        {
            A.CallTo(() => fakeBarRepository.getBars())
                            .Returns((new List<Bar>() { new Bar() { Id = 1, BarName = "TestBar", Address = "TestAddress" } }).AsQueryable());

            List<BarResponseModel> bars = barService.getBars();
            Assert.IsInstanceOf<List<BarResponseModel>>(bars);
            Assert.AreEqual(1,bars.Count);
        }

    }
}
