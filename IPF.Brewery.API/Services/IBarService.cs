using FluentValidation.Results;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Services
{
    public interface IBarService
    {
        ValidationResult validateBar(VMBar vmBar);
        BarResponseModel? getBar(int breweryId);
        List<BarResponseModel> getBars();
        List<BarBeer> getBarBeers(int barId);
        List<BarBeer> getAllBarsWithBeers();
        int addBar(BarPayload barPayload);
        int updateBar(int beerId, BarPayload barPayload);
        ValidationResult validateBarBeer(VMBarBeer vmBarBeer);
        int addBarBeer(BarBeerPayload barBeerPayload);
    }
}
