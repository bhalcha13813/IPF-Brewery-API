﻿using System.Net;
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
    public class BeerServiceTests
    {
        private IAddBeerValidator fakeAddBeerValidator;
        private IBeerRepository fakeBeerRepository;
        private IBeerService beerService;

        [SetUp]
        public void Setup()
        {
            fakeAddBeerValidator = A.Fake<IAddBeerValidator>();
            fakeBeerRepository = A.Fake<IBeerRepository>();
            beerService = new BeerService(fakeAddBeerValidator, fakeBeerRepository);
        }

        [Test]
        public void Test_validateAddBeer_Returns_Error_When_InvalidPayload()
        {
            var validationMessage = new ValidationFailure("prop1", "error message");
            validationMessage.ErrorCode = HttpStatusCode.BadRequest.ToString();

            A.CallTo(() => fakeAddBeerValidator.Validate(A<BeerPayload>.Ignored))
                            .Returns(new ValidationResult(new List<ValidationFailure> { validationMessage }));

            var result = beerService.validateAddBeer(new BeerPayload());
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("error message", result.Errors.Single().ErrorMessage);
        }

        [Test] 
        public void Test_validateAddBeer_Returns_Success_When_ValidPayload()
        {
            A.CallTo(() => fakeAddBeerValidator.Validate(A<BeerPayload>.Ignored))
                 .Returns(new ValidationResult(new List<ValidationFailure>()));

            var result = beerService.validateAddBeer(new BeerPayload());
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Test_GetBeers_Returns_Beers()
        {
            A.CallTo(() => fakeBeerRepository.getBeers(A<decimal>.Ignored, A<decimal>.Ignored))
                            .Returns((new List<Beer>() { new Beer() { Id = 1, BeerName = "TestBeer", PercentageAlcoholByVolume = 5.5M, BeerTypeId = 1, 
                                              BeerType = new BeerType() {Id = 1, BeerTypeName = "Beer Type"} } }).AsQueryable());

            List<BeerResponseModel> beers = beerService.getBeers(5.0M, 6.5M);
            Assert.IsInstanceOf<List<BeerResponseModel>>(beers);
            Assert.AreEqual(1, beers.Count);
        }

    }
}
