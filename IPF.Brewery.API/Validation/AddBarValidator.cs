using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;

namespace IPF.Brewery.API.Validation
{
    public interface IAddBarValidator
    {
        ValidationResult Validate(BarPayload barPayload);

    }

    public class AddBarValidator : AbstractValidator<BarPayload>, IAddBarValidator
    {
        private readonly IBarRepository barRepository;

        private Bar bar;

        ValidationResult IAddBarValidator.Validate(BarPayload barPayload)
        {
            return Validate(barPayload);
        }

        public AddBarValidator(IBarRepository barRepository)
        {
            this.barRepository = barRepository;

            RuleFor(b => b.BarName).NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("BarName cannot be empty.");

            RuleFor(b => b.BarName)
                .Must(b => BeUniqueBarName(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("BarName already exists.");
        }

        private Bar getBar(string barName)
        {
            if (bar == null)
            {
                bar = barRepository.getBar(barName);
            }

            return bar;
        }

        private bool BeUniqueBarName(string barName)
        { 
            bar = getBar(barName);
            return bar == null;
        }
    }
}
