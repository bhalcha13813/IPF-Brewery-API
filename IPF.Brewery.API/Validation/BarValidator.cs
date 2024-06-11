using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.API.Validation
{
    public interface IBarValidator
    {
        ValidationResult Validate(VMBar vmBar);
    }

    public class BarValidator : AbstractValidator<VMBar>, IBarValidator
    {
        private readonly IBarRepository barRepository;

        private Bar bar;

        ValidationResult IBarValidator.Validate(VMBar vmBar)
        {
            return Validate(vmBar);
        }

        public BarValidator(IBarRepository barRepository)
        {
            this.barRepository = barRepository;

            RuleFor(b => b.BarName).NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("BarName cannot be empty.");

            RuleFor(b => b)
                .Must(b => BeUniqueBarName(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("BarName already exists.")
                .OverridePropertyName(b => b.BarName);
        }

        private Bar getBar(VMBar vmBar)
        {
            if (bar == null)
            {
                bar = barRepository.getBar(vmBar.BarName);

                if (bar != null && vmBar.Id != null)
                {
                    if (vmBar.Id.Value == bar.Id)
                    {
                        bar = null;
                    }
                }
            }

            return bar;
        }

        private bool BeUniqueBarName(VMBar vmBar)
        { 
            bar = getBar(vmBar);
            return bar == null;
        }
    }
}
