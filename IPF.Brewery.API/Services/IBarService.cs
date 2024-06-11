﻿using FluentValidation.Results;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Services
{
    public interface IBarService
    {
        ValidationResult validateAddBar(BarPayload barPayload);
        BarResponseModel? getBar(int breweryId);
        List<BarResponseModel> getBars();
        List<BarBeer> getBarBeers(int barId);
        List<BarBeer> getAllBarsWithBeers();
        int addBar(BarPayload barPayload);
        int updateBar(int beerId, BarPayload barPayload);
        int addBarBeer(BarBeerPayload barBeerPayload);
    }
}
