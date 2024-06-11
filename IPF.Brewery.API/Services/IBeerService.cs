using FluentValidation.Results;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Services
{
    public interface IBeerService
    {
        BeerResponseModel? getBeer(int beerId);
        List<BeerResponseModel> getBeers();
        List<BeerResponseModel> getBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
        ValidationResult validateBeer(VMBeer vmBeer);
        int addBeer(BeerPayload beerPayload);
        int updateBeer(int beerId, BeerPayload beerPayload);
    }
}
