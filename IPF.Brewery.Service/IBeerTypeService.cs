using FluentValidation.Results;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Service
{
    public interface IBeerTypeService
    {
        List<BeerTypeResponseModel> GetBeerTypes();
        ValidationResult ValidateBeerType(VMBeerType vmBeerType);
        int AddBeerType(BeerTypePayload beerTypePayload);
        int UpdateBeerType(int beerId, BeerTypePayload beerTypePayload);
    }
}
