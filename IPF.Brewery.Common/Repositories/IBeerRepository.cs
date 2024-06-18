using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.Common.Repositories
{
    public interface IBeerRepository
    {
        Beer? GetBeer(int beerId);
        Beer? GetBeer(string beerName);
        IQueryable<Beer> GetBeers();
        IQueryable<Beer> GetBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
        int AddBeer(Beer beer);
        int UpdateBeer(Beer beer);
    }
}
