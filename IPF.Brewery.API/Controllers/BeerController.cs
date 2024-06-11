using FluentValidation.Results;
using IPF.Brewery.API.Extension;
using IPF.Brewery.API.Services;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace IPF.Brewery.API.Controllers
{
    public class BeerController : BaseController
    {
        private readonly IBeerService beerService;
        public BeerController(IHttpContextAccessor contextAccessor, 
                              IBeerService beerService) : base(contextAccessor)
        {
            this.beerService = beerService;
        }

        [HttpGet]
        [Route("/Beer/{beerId}")]
        public IActionResult GetBeer(int beerId)
        {
            BeerResponseModel? beer = beerService.getBeer(beerId);
            return new OkObjectResult(beer);
        }

        [HttpGet]
        [Route("/Beer/All")]
        public IActionResult GetBeers()
        {
            List<BeerResponseModel> beers = beerService.getBeers();
            return new OkObjectResult(beers);
        }

        [HttpGet]
        [Route("/Beer")]
        public IActionResult GetBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            List<BeerResponseModel> beers = beerService.getBeers(gtAlcoholByVolume, ltAlcoholByVolume);
            return new OkObjectResult(beers);
        }

        [HttpGet]
        [Route("/Beer/Types")]
        public IActionResult GetBeerTypes()
        {
            List<BeerTypeResponseModel> beerTypes = beerService.getBeerTypes();
            return new OkObjectResult(beerTypes);
        }

        [HttpPost]
        [Route("/Beer")]
        public IActionResult AddBeer(BeerPayload beerPayload)
        {
            ValidationResult validationResult = beerService.validateAddBeer(beerPayload);

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
            beerService.addBeer(beerPayload);
            return new OkResult();
        }

        [HttpPut]
        [Route("/Beer/{beerId}")]
        public IActionResult UpdateBeer(int beerId, BeerPayload beerPayload)
        {
            beerService.updateBeer(beerId, beerPayload);
            return new OkResult();
        }
    }
}
