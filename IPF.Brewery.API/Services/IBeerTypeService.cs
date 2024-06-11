using FluentValidation.Results;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Services
{
    public interface IBeerTypeService
    {
        List<BeerTypeResponseModel> getBeerTypes();
        ValidationResult validateBeerType(VMBeerType vmBeerType);
        int addBeerType(BeerTypePayload beerTypePayload);
        int updateBeerType(int beerId, BeerTypePayload beerTypePayload);
    }
}
