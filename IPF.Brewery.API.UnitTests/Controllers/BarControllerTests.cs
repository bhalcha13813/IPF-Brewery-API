using System.Net;
using FakeItEasy;
using FluentValidation.Results;
using IPF.Brewery.API.Controllers;
using IPF.Brewery.API.Service;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPF.Brewery.API.UnitTests.Controllers
{
    [TestFixture()]
    public class BarControllerTests
    {
        private BarController barController;
        private IHttpContextAccessor fakeHttpContextAccessor;
        private IBarService fakeBarService;

        [SetUp]
        public void Setup()
        {
            fakeHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            fakeBarService = A.Fake<IBarService>();
            A.CallTo(() => fakeHttpContextAccessor.HttpContext).Returns(new DefaultHttpContext());
            barController = new BarController(fakeHttpContextAccessor, fakeBarService);
        }

        [Test]
        public void Test_AddBar_Returns_BadRequest_When_InvalidRequest()
        {
            var validationMessage = new ValidationFailure("prop1", "error message");
            validationMessage.ErrorCode = HttpStatusCode.BadRequest.ToString();

            A.CallTo(() => fakeBarService.ValidateBar(A<VMBar>.Ignored))
                            .Returns(new ValidationResult(new List<ValidationFailure> { validationMessage }));

            var result = (BadRequestObjectResult) barController.AddBar(new BarPayload());
            var errors = (ErrorDescription)result.Value;
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("error message", errors.Errors.Single().Description);
        }

        [Test] 
        public void Test_AddBar_Returns_OkResult_When_ValidRequest()
        {
            A.CallTo(() => fakeBarService.ValidateBar(A<VMBar>.Ignored))
                 .Returns(new ValidationResult(new List<ValidationFailure>()));

            A.CallTo(() => fakeBarService.AddBar(A<BarPayload>.Ignored))
                 .Returns(1);

            var result = (OkResult) barController.AddBar(new BarPayload());
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Test_GetBars_Returns_OkObjectResult_When_ValidRequest()
        {
            A.CallTo(() => fakeBarService.GetBars())
                            .Returns(new List<BarResponseModel>() { new BarResponseModel() { Id = 1, BarName = "TestBar", Address = "TestAddress"} });

            var result = (OkObjectResult)barController.GetBars();
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(1, ((List<BarResponseModel>?)result.Value).Count);
        }

    }
}
