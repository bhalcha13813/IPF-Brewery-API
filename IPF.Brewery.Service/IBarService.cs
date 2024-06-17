using FluentValidation.Results;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Services
{
    public interface IBarService
    {
        ValidationResult ValidateBar(VMBar vmBar);
        BarResponseModel? GetBar(int breweryId);
        List<BarResponseModel> GetBars();
        BarBeer? GetBarBeers(int barId);
        List<BarBeer> GetAllBarsWithBeers();
        int AddBar(BarPayload barPayload);
        int UpdateBar(int beerId, BarPayload barPayload);
        ValidationResult ValidateBarBeer(VMBarBeer vmBarBeer);
        int AddBarBeer(BarBeerPayload barBeerPayload);
    }
}
