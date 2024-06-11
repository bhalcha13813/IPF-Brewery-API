using FluentValidation;
using FluentValidation.Results;
using System.Net;
using FluentValidation.Validators;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Extension
{
    public static class FluentValidationResultExtensions
    {
        public static bool HasConflictErrors(this ValidationResult validationResult, out List<Error> conflictErrors)
        {
            conflictErrors = new List<Error>();
            return HasErrors(validationResult, HttpStatusCode.Conflict, out conflictErrors);
        }

        public static bool HasBadRequestErrors(this ValidationResult validationResult, out List<Error> badRequestErrors)
        {
            badRequestErrors = new List<Error>();
            return HasErrors(validationResult, HttpStatusCode.BadRequest, out badRequestErrors);
        }

        public static bool HasGoneErrors(this ValidationResult validationResult, out List<Error> goneErrors)
        {
            goneErrors = new List<Error>();
            return HasErrors(validationResult, HttpStatusCode.Gone, out goneErrors);
        }

        public static bool HasRequestedRangeNotSatisfiable(this ValidationResult validationResult, out List<Error> rangeNotSatisfiable)
        {
            rangeNotSatisfiable = new List<Error>();
            return HasErrors(validationResult, HttpStatusCode.RequestedRangeNotSatisfiable, out rangeNotSatisfiable);
        }

        public static bool HasNotFoundErrors(this ValidationResult validationResult, out List<Error> notFoundErrors)
        {
            notFoundErrors = new List<Error>();
            return HasErrors(validationResult, HttpStatusCode.NotFound, out notFoundErrors);
        }

        public static bool HasUnauthorizedErrors(this ValidationResult validationResult, out List<Error> notFoundErrors)
        {
            notFoundErrors = new List<Error>();
            return HasErrors(validationResult, HttpStatusCode.Unauthorized, out notFoundErrors);
        }

        private static bool HasErrors(this ValidationResult validationResult, HttpStatusCode errorCode, out List<Error> errors)
        {
            errors = new List<Error>();
            if (validationResult.Errors.Any(e => e.ErrorCode.Equals(errorCode.ToString())))
            {
                errors = validationResult.Errors.Where(e => e.ErrorCode.Equals(errorCode.ToString()))
                                    .Select(f => new Error { Source = f.PropertyName, Description = f.ToString() })
                                    .ToList();
                return true;
            }
            return false;
        }

        public static IRuleBuilderOptions<T, object> SetPropertyValidator<T>(this IRuleBuilder<T, object> ruleBuilder, 
            IPropertyValidator propertyValidator)
        {
            return ruleBuilder.SetValidator((IPropertyValidator<T, object>)propertyValidator);
        }
    }
}
