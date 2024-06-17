using FluentValidation.Results;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Service
{
    public interface IBreweryService
    {
        ValidationResult ValidateBrewery(VMBrewery vmBrewery);
        BreweryResponseModel? GetBrewery(int breweryId);
        List<BreweryResponseModel> GetBreweries();
        BreweryBeer? GetBreweryBeers(int breweryId);
        List<BreweryBeer> GetAllBreweriesWithBeers();
        int AddBrewery(BreweryPayload breweryPayload);
        int UpdateBrewery(int beerId, BreweryPayload breweryPayload);
        ValidationResult ValidateBreweryBeer(VMBreweryBeer vmBreweryBeer);
        int AddBreweryBeer(BreweryBeerPayload breweryBeerPayload);
    }
}
