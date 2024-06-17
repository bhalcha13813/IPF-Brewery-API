using FluentValidation.Results;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Service
{
    public interface IBeerService
    {
        BeerResponseModel? GetBeer(int beerId);
        List<BeerResponseModel> GetBeers();
        List<BeerResponseModel> GetBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
        ValidationResult ValidateBeer(VMBeer vmBeer);
        int AddBeer(BeerPayload beerPayload);
        int UpdateBeer(int beerId, BeerPayload beerPayload);
    }
}
