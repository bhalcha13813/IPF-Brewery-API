﻿using FluentValidation.Results;
using IPF.Brewery.API.Extension;
using IPF.Brewery.API.Services;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IPF.Brewery.API.Controllers
{
    public class BreweryController : BaseController
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBreweryService breweryService; 
        public BreweryController(IHttpContextAccessor httpContextAccessor,
                                 IBreweryService breweryService) : base(httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.breweryService = breweryService;
        }

        [HttpGet]
        [Route("/brewery")]
        public IActionResult GetBreweries()
        {
           List<BreweryResponseModel> breweries = breweryService.getBreweries();
           return new OkObjectResult(breweries);
        }

        [HttpGet]
        [Route("/brewery/{breweryId}")]
        public IActionResult GetBrewery(int breweryId)
        {
            BreweryResponseModel? brewery = breweryService.getBrewery(breweryId);
            return new OkObjectResult(brewery);
        }

        [HttpGet]
        [Route("/brewery/{breweryId}/beer")]
        public IActionResult GetBreweryBeers(int breweryId)
        {
            List<BreweryBeer> breweryBeers = breweryService.getBreweryBeers(breweryId);
            return new OkObjectResult(breweryBeers);
        }

        [HttpGet]
        [Route("/brewery/beer")]
        public IActionResult getAllBreweriesWithBeers()
        {
            List<BreweryBeer> allbreweriesWithBeers = breweryService.getAllBreweriesWithBeers();
            return new OkObjectResult(allbreweriesWithBeers);
        }

        [HttpPost]
        [Route("/brewery")]
        public IActionResult AddBrewery(BreweryPayload breweryPayload)
        {
            ValidationResult validationResult = breweryService.validateAddBrewery(breweryPayload);

            if (!validationResult.IsValid)
            {
                List<Error> errors;

                if (validationResult.HasConflictErrors(out errors))
                {
                    return BuildConflictErrorResponse(errors);
                }

                if (validationResult.HasBadRequestErrors(out errors))
                {
                    return BuildBadRequestErrorResponse(errors);
                }
            }

            breweryService.addBrewery(breweryPayload);
            return new OkResult();
        }

        [HttpPut]
        [Route("/brewery/{breweryId}")]
        public IActionResult UpdateBrewery(int breweryId, BreweryPayload beerPayload)
        {
            breweryService.updateBrewery(breweryId, beerPayload);
            return new OkResult();
        }

        [HttpPost]
        [Route("/brewery/beer")]
        public IActionResult AddBreweryBeer(BreweryBeerPayload breweryBeerPayload)
        {
            breweryService.addBreweryBeer(breweryBeerPayload);
            return new OkResult();
        }
    }
}
