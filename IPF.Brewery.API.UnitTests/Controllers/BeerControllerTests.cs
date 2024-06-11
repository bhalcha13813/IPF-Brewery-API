using System.Net;
using FakeItEasy;
using FluentValidation.Results;
using IPF.Brewery.API.Controllers;
using IPF.Brewery.API.Services;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPF.Brewery.API.UnitTests.Controllers
{
    [TestFixture()]
    public class BeerControllerTests
    {
        private BeerController beerController;
        private IHttpContextAccessor fakeHttpContextAccessor;
        private IBeerService fakeBeerService;

        [SetUp]
        public void Setup()
        {
            fakeHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            fakeBeerService = A.Fake<IBeerService>();
            A.CallTo(() => fakeHttpContextAccessor.HttpContext).Returns(new DefaultHttpContext());
            beerController = new BeerController(fakeHttpContextAccessor, fakeBeerService);
        }

        [Test]
        public void Test_AddBeer_Returns_BadRequest_When_InvalidRequest()
        {
            var validationMessage = new ValidationFailure("prop1", "error message");
            validationMessage.ErrorCode = HttpStatusCode.BadRequest.ToString();

            A.CallTo(() => fakeBeerService.validateBeer(A<VMBeer>.Ignored))
                            .Returns(new ValidationResult(new List<ValidationFailure> { validationMessage }));

            var result = (BadRequestObjectResult) beerController.AddBeer(new BeerPayload());
            var errors = (ErrorDescription)result.Value;
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("error message", errors.Errors.Single().Description);
        }

        [Test] 
        public void Test_AddBeer_Returns_OkResult_When_ValidRequest()
        {
            A.CallTo(() => fakeBeerService.validateBeer(A<VMBeer>.Ignored))
                 .Returns(new ValidationResult(new List<ValidationFailure>()));

            A.CallTo(() => fakeBeerService.addBeer(A<BeerPayload>.Ignored))
                 .Returns(1);

            var result = (OkResult) beerController.AddBeer(new BeerPayload());
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Test_GetBeers_Returns_OkObjectResult_When_ValidRequest()
        {
            A.CallTo(() => fakeBeerService.getBeers())
                            .Returns(new List<BeerResponseModel>() { new BeerResponseModel() { Id = 1, BeerName = "TestBeer", PercentageAlcoholByVolume = 4.5M, BeerType = "Beer Type 1"} });

            var result = (OkObjectResult)beerController.GetBeers();
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(1,((List<BeerResponseModel>?)result.Value).Count);
        }

    }
}
