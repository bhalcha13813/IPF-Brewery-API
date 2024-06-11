using System.Net;
using FakeItEasy;
using FluentValidation.Results;
using IPF.Brewery.API.Services;
using IPF.Brewery.API.Validation;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;

namespace IPF.Brewery.API.UnitTests.Services
{
    [TestFixture()]
    public class BreweryServiceTests
    {
        private IBreweryValidator fakeBreweryValidator;
        private IBreweryBeerValidator fakeBreweryBeerValidator;
        private IBreweryRepository fakeBreweryRepository;
        private IBeerRepository fakeBeerRepository;
        private IBreweryService breweryService;

        [SetUp]
        public void Setup()
        {
            fakeBreweryValidator = A.Fake<IBreweryValidator>();
            fakeBreweryBeerValidator = A.Fake<IBreweryBeerValidator>();
            fakeBreweryRepository = A.Fake<IBreweryRepository>();
            fakeBeerRepository = A.Fake<IBeerRepository>();
            breweryService = new BreweryService(fakeBreweryValidator, fakeBreweryBeerValidator, fakeBreweryRepository, fakeBeerRepository);
        }

        [Test]
        public void Test_validateAddBrewery_Returns_Error_When_InvalidPayload()
        {
            var validationMessage = new ValidationFailure("prop1", "error message");
            validationMessage.ErrorCode = HttpStatusCode.BadRequest.ToString();

            A.CallTo(() => fakeBreweryValidator.Validate(A<VMBrewery>.Ignored))
                            .Returns(new ValidationResult(new List<ValidationFailure> { validationMessage }));

            var result =  breweryService.validateBrewery(new VMBrewery());
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("error message", result.Errors.Single().ErrorMessage);
        }

        [Test] 
        public void Test_validateAddBrewery_Returns_Success_When_ValidPayload()
        {
            A.CallTo(() => fakeBreweryValidator.Validate(A<VMBrewery>.Ignored))
                 .Returns(new ValidationResult(new List<ValidationFailure>()));

            var result = breweryService.validateBrewery(new VMBrewery());
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Test_GetBreweries_Returns_Breweries()
        {
            A.CallTo(() => fakeBreweryRepository.getBreweries())
                            .Returns((new List<Common.Models.DTO.Brewery>() { new Common.Models.DTO.Brewery() { Id = 1, BreweryName = "TestBrewery", Address = "TestAddress" } }).AsQueryable());

            List<BreweryResponseModel> breweries = breweryService.getBreweries();
            Assert.IsInstanceOf<List<BreweryResponseModel>>(breweries);
            Assert.AreEqual(1, breweries.Count);
        }

    }
}
