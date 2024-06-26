﻿using System.Net;
using AutoMapper;
using FakeItEasy;
using FluentValidation.Results;
using IPF.Brewery.API.Service;
using IPF.Brewery.API.Validation;
using IPF.Brewery.API.Validation.Models;
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
        private IMapper fakeMapper;

        [SetUp]
        public void Setup()
        {
            fakeBreweryValidator = A.Fake<IBreweryValidator>();
            fakeBreweryBeerValidator = A.Fake<IBreweryBeerValidator>();
            fakeBreweryRepository = A.Fake<IBreweryRepository>();
            fakeBeerRepository = A.Fake<IBeerRepository>();
            fakeMapper = A.Fake<IMapper>();
            breweryService = new BreweryService(fakeBreweryValidator, fakeBreweryBeerValidator, fakeBreweryRepository, fakeBeerRepository, fakeMapper);
        }

        [Test]
        public void Test_validateAddBrewery_Returns_Error_When_InvalidPayload()
        {
            var validationMessage = new ValidationFailure("prop1", "error message");
            validationMessage.ErrorCode = HttpStatusCode.BadRequest.ToString();

            A.CallTo(() => fakeBreweryValidator.Validate(A<VMBrewery>.Ignored))
                            .Returns(new ValidationResult(new List<ValidationFailure> { validationMessage }));

            var result =  breweryService.ValidateBrewery(new VMBrewery());
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("error message", result.Errors.Single().ErrorMessage);
        }

        [Test] 
        public void Test_validateAddBrewery_Returns_Success_When_ValidPayload()
        {
            A.CallTo(() => fakeBreweryValidator.Validate(A<VMBrewery>.Ignored))
                 .Returns(new ValidationResult(new List<ValidationFailure>()));

            var result = breweryService.ValidateBrewery(new VMBrewery());
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Test_GetBreweries_Returns_Breweries()
        {
            A.CallTo(() => fakeBreweryRepository.GetBreweries())
                            .Returns((new List<Common.Models.DTO.Brewery>() { new Common.Models.DTO.Brewery() { Id = 1, BreweryName = "TestBrewery", Address = "TestAddress" } }).AsQueryable());

            List<BreweryResponseModel> breweries = breweryService.GetBreweries();
            Assert.IsInstanceOf<List<BreweryResponseModel>>(breweries);
            Assert.AreEqual(1, breweries.Count);
        }

    }
}
