using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Validation
{
    public interface IBreweryBeerValidator
    {
        ValidationResult Validate(VMBreweryBeer vmBreweryBeer);
    }

    public class BreweryBeerValidator : AbstractValidator<VMBreweryBeer>, IBreweryBeerValidator
    {
        private readonly IBreweryRepository breweryRepository;
        private readonly IBeerRepository beerRepository;

        private Common.Models.DTO.Brewery brewery;
        private Beer beer;

        ValidationResult IBreweryBeerValidator.Validate(VMBreweryBeer vmBreweryBeer)
        {
            return Validate(vmBreweryBeer);
        }

        public BreweryBeerValidator(IBreweryRepository breweryRepository, IBeerRepository beerRepository)
        {
            this.breweryRepository = breweryRepository;
            this.beerRepository = beerRepository;

            RuleFor(b => b.BreweryId)
                .Must(b => BeExistingBrewery(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("Brewery does not exist, Please add Brewery first.");

            RuleFor(b => b.BeerId)
                .Must(b => BeExistingBeer(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("Beer does not exist, Please add Beer first.");
        }

        private Common.Models.DTO.Brewery getBrewery(int breweryId)
        {
            if (brewery == null)
            {
                brewery = breweryRepository.getBrewery(breweryId);
            }

            return brewery;
        }

        private Beer getBeer(int beerId)
        {
            if (beer == null)
            {
                beer = beerRepository.getBeer(beerId);
            }

            return beer;
        }

        private bool BeExistingBrewery(int breweryId)
        {
            brewery = getBrewery(breweryId);
            return brewery != null;
        }

        private bool BeExistingBeer(int beerId)
        {
            beer = getBeer(beerId);
            return beer != null;
        }
    }
}
