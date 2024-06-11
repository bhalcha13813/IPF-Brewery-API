using FluentValidation.Results;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Services
{
    public interface IBeerService
    {
        ValidationResult validateAddBeer(BeerPayload beerPayload);
        BeerResponseModel? getBeer(int beerId);
        List<BeerResponseModel> getBeers();
        List<BeerResponseModel> getBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
        List<BeerTypeResponseModel> getBeerTypes();
        int addBeer(BeerPayload beerPayload);
        int updateBeer(int beerId, BeerPayload beerPayload);
    }
}
