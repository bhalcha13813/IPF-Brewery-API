namespace IPF.Brewery.Common.Repositories
{
    public interface IBreweryRepository
    {
        Models.DTO.Brewery? GetBrewery(int breweryId);
        Models.DTO.Brewery? GetBrewery(string breweryName);
        IQueryable<Models.DTO.Brewery> GetBreweries();
        Models.DTO.Brewery? GetBreweryBeers(int breweryId);
        IQueryable<Models.DTO.Brewery> GetAllBreweriesWithBeers();
        int AddBrewery(Models.DTO.Brewery brewery);
        int UpdateBrewery(Models.DTO.Brewery brewery);
    }
}
