namespace IPF.Brewery.Common.Repositories
{
    public interface IBreweryRepository
    {
        Models.DTO.Brewery? getBrewery(int breweryId);
        Models.DTO.Brewery? getBrewery(string breweryName);
        IQueryable<Models.DTO.Brewery> getBreweries();
        IQueryable<Models.DTO.Brewery> getBreweryBeers(int breweryId);
        IQueryable<Models.DTO.Brewery> getAllBreweriesWithBeers();
        int addBrewery(Models.DTO.Brewery brewery);
        int updateBrewery(Models.DTO.Brewery brewery);
    }
}
