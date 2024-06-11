using FluentValidation.Results;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Services
{
    public interface IBreweryService
    {
        ValidationResult validateAddBrewery(BreweryPayload breweryPayload);
        BreweryResponseModel? getBrewery(int breweryId);
        List<BreweryResponseModel> getBreweries();
        List<BreweryBeer> getBreweryBeers(int breweryId);
        List<BreweryBeer> getAllBreweriesWithBeers();
        int addBrewery(BreweryPayload breweryPayload);
        int updateBrewery(int beerId, BreweryPayload breweryPayload);
        int addBreweryBeer(BreweryBeerPayload breweryBeerPayload);
    }
}
