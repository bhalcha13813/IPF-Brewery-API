using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.API.Validation.Models;

namespace IPF.Brewery.API.Validation
{
    public interface IBeerValidator
    {
        ValidationResult Validate(VMBeer vmBeer);

    }

    public class BeerValidator : AbstractValidator<VMBeer>, IBeerValidator
    {
        private readonly IBeerRepository beerRepository;
        private readonly IBeerTypeRepository beerTypeRepository;

        private Beer beer;
        private BeerType beerType;

        ValidationResult IBeerValidator.Validate(VMBeer vmBeer)
        {
            return Validate(vmBeer);
        }

        public BeerValidator(IBeerRepository beerRepository, IBeerTypeRepository beerTypeRepository)
        {
            this.beerRepository = beerRepository;
            this.beerTypeRepository = beerTypeRepository;

            RuleFor(b => b.BeerName).NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("BeerName cannot be empty.");

            RuleFor(b => b)
                .Must(b => BeUniqueBeerName(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("BeerName already exists.")
                .OverridePropertyName(f => f.BeerName);

            RuleFor(b => b.BeerTypeId)
                .Must(b => BeExistingBeerType(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("BeerType does not exist, Please add BeerType first.");
        }

        private Beer getBeer(VMBeer vmBeer)
        {
            if (beer == null)
            {
                beer = beerRepository.getBeer(vmBeer.BeerName);

                if (beer != null && vmBeer.Id != null)
                {
                    if (vmBeer.Id.Value == beer.Id)
                    {
                        beer = null;
                    }
                }
            }

            return beer;
        }

        private BeerType getBeerType(int beerTypeId)
        {
            if (beerType == null)
            {
                beerType = beerTypeRepository.getBeerType(beerTypeId);
            }

            return beerType;
        }

        private bool BeUniqueBeerName(VMBeer vmBeer)
        { 
            beer = getBeer(vmBeer);
            return beer == null;
        }

        private bool BeExistingBeerType(int beerTypeId)
        {
            BeerType beerType = getBeerType(beerTypeId);
            return beerType != null;
        }
    }
}
