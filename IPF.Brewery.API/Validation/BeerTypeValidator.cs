using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.API.Validation
{
    public interface IBeerTypeValidator
    {
        ValidationResult Validate(VMBeerType vmBeerType);

    }

    public class BeerTypeValidator : AbstractValidator<VMBeerType>, IBeerTypeValidator
    {
        private readonly IBeerTypeRepository beerTypeRepository;

        private BeerType? beerType;

        ValidationResult IBeerTypeValidator.Validate(VMBeerType vmBeerType)
        {
            return Validate(vmBeerType);
        }

        public BeerTypeValidator(IBeerTypeRepository beerTypeRepository)
        {
            this.beerTypeRepository = beerTypeRepository;

            RuleFor(b => b.BeerType).NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("BeerType cannot be empty.");

            RuleFor(b => b)
                .Must(b => BeUniqueBeerType(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("BeerType already exists.");
        }

        private BeerType? getBeerType(VMBeerType vmBeerType)
        {
            if (beerType == null)
            {
                beerType = beerTypeRepository.getBeerType(vmBeerType.BeerType);

                if (beerType != null && vmBeerType.Id != null)
                {
                    if (vmBeerType.Id.Value == beerType.Id)
                    {
                        beerType = null;
                    }
                }
            }

            return beerType;
        }

        private bool BeUniqueBeerType(VMBeerType vmBeerType)
        { 
            beerType = getBeerType(vmBeerType);
            return beerType == null;
        }
    }
}
