using System.Net;
using AutoMapper;
using FakeItEasy;
using FluentValidation.Results;
using IPF.Brewery.API.Service;
using IPF.Brewery.API.Validation;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;

namespace IPF.Brewery.API.UnitTests.Services
{
    [TestFixture()]
    public class BarServiceTests
    {
        private IBarValidator fakeBarValidator;
        private IBarBeerValidator fakeBarBeerValidator;
        private IBarRepository fakeBarRepository;
        private IBeerRepository fakeBeerRepository;
        private IBarService barService;
        private IMapper fakeMapper;

        [SetUp]
        public void Setup()
        {
            fakeBarValidator = A.Fake<IBarValidator>();
            fakeBarBeerValidator = A.Fake<IBarBeerValidator>();
            fakeBarRepository = A.Fake<IBarRepository>();
            fakeBeerRepository = A.Fake<IBeerRepository>();
            fakeMapper = A.Fake<IMapper>();
            barService = new BarService(fakeBarValidator, fakeBarBeerValidator, fakeBarRepository, fakeBeerRepository, fakeMapper);
        }

        [Test]
        public void Test_validateAddBar_Returns_Error_When_InvalidPayload()
        {
            var validationMessage = new ValidationFailure("prop1", "error message");
            validationMessage.ErrorCode = HttpStatusCode.BadRequest.ToString();

            A.CallTo(() => fakeBarValidator.Validate(A<VMBar>.Ignored))
                            .Returns(new ValidationResult(new List<ValidationFailure> { validationMessage }));

            var result =  barService.ValidateBar(new VMBar());
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("error message", result.Errors.Single().ErrorMessage);
        }

        [Test] 
        public void Test_validateAddBar_Returns_Success_When_ValidPayload()
        {
            A.CallTo(() => fakeBarValidator.Validate(A<VMBar>.Ignored))
                 .Returns(new ValidationResult(new List<ValidationFailure>()));

            var result = barService.ValidateBar(new VMBar());
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Test_GetBars_Returns_Bars()
        {
            A.CallTo(() => fakeBarRepository.GetBars())
                            .Returns((new List<Bar>() { new Bar() { Id = 1, BarName = "TestBar", Address = "TestAddress" } }).AsQueryable());

            List<BarResponseModel> bars = barService.GetBars();
            Assert.IsInstanceOf<List<BarResponseModel>>(bars);
            Assert.AreEqual(1,bars.Count);
        }

    }
}
