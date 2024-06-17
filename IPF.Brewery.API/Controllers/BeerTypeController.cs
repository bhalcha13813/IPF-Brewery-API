using FluentValidation.Results;
using IPF.Brewery.API.Extension;
using IPF.Brewery.API.Service;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Mvc;
using VMBeerType = IPF.Brewery.API.Validation.Models.VMBeerType;

namespace IPF.Brewery.API.Controllers
{
    public class BeerTypeController : BaseController
    {
        private readonly IBeerTypeService beerTypeService;
        public BeerTypeController(IHttpContextAccessor contextAccessor, 
                              IBeerTypeService beerTypeService) : base(contextAccessor)
        {
            this.beerTypeService = beerTypeService;
        }


        [HttpGet]
        [Route("/BeerType")]
        public IActionResult GetBeerTypes()
        {
            List<BeerTypeResponseModel> beerTypes = beerTypeService.GetBeerTypes();
            return new OkObjectResult(beerTypes);
        }

        [HttpPost]
        [Route("/BeerType")]
        public IActionResult AddBeerType(BeerTypePayload beerTypePayload)
        {
            VMBeerType beerType = new VMBeerType() {BeerType = beerTypePayload.BeerType};
            ValidationResult validationResult = beerTypeService.ValidateBeerType(beerType);

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
            beerTypeService.AddBeerType(beerTypePayload);
            return new OkResult();
        }

        [HttpPut]
        [Route("/BeerType/{beerTypeId}")]
        public IActionResult UpdateBeer(int beerTypeId, BeerTypePayload beerTypePayload)
        {
            VMBeerType beerType = new VMBeerType() { Id = beerTypeId ,BeerType = beerTypePayload.BeerType };
            ValidationResult validationResult = beerTypeService.ValidateBeerType(beerType);

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

            beerTypeService.UpdateBeerType(beerTypeId, beerTypePayload);
            return new OkResult();
        }
    }
}
