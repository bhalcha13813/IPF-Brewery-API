using System.Net;
using Castle.Core.Logging;
using FakeItEasy;
using FluentValidation.Results;
using IPF.Brewery.API.Controllers;
using IPF.Brewery.API.Service;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IPF.Brewery.API.UnitTests.Controllers
{
    [TestFixture()]
    public class BreweryControllerTests
    {
        private BreweryController breweryController;
        private IHttpContextAccessor fakeHttpContextAccessor;
        private ILogger<BreweryController> fakeLogger;
        private IBreweryService fakeBreweryService;

        [SetUp]
        public void Setup()
        {
            fakeHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            fakeLogger = A.Fake<ILogger<BreweryController>>();
            fakeBreweryService = A.Fake<IBreweryService>();
            A.CallTo(() => fakeHttpContextAccessor.HttpContext).Returns(new DefaultHttpContext());
            breweryController = new BreweryController(fakeHttpContextAccessor, fakeLogger, fakeBreweryService);
        }

        [Test]
        public void Test_AddBrewery_Returns_BadRequest_When_InvalidRequest()
        {
            var validationMessage = new ValidationFailure("prop1", "error message");
            validationMessage.ErrorCode = HttpStatusCode.BadRequest.ToString();

            A.CallTo(() => fakeBreweryService.ValidateBrewery(A<VMBrewery>.Ignored))
                            .Returns(new ValidationResult(new List<ValidationFailure> { validationMessage }));

            var result = (BadRequestObjectResult) breweryController.AddBrewery(new BreweryPayload());
            var errors = (ErrorDescription)result.Value;
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("error message", errors.Errors.Single().Description);
        }

        [Test] 
        public void Test_AddBrewery_Returns_OkResult_When_ValidRequest()
        {
            A.CallTo(() => fakeBreweryService.ValidateBrewery(A<VMBrewery>.Ignored))
                 .Returns(new ValidationResult(new List<ValidationFailure>()));

            A.CallTo(() => fakeBreweryService.AddBrewery(A<BreweryPayload>.Ignored))
                 .Returns(1);

            var result = (OkResult) breweryController.AddBrewery(new BreweryPayload());
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Test_GetBreweries_Returns_OkObjectResult_When_ValidRequest()
        {
            A.CallTo(() => fakeBreweryService.GetBreweries())
                            .Returns(new List<BreweryResponseModel>() { new BreweryResponseModel() { Id = 1, BreweryName = "TestBrewery", Address = "TestAddress"} });

            var result = (OkObjectResult)breweryController.GetBreweries();
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(1, ((List<BreweryResponseModel>?)result.Value).Count);
        }

    }
}
