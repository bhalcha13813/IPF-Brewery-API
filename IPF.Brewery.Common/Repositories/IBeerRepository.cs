using IPF.Brewery.Common.Models.DTO;
using System.Linq;

namespace IPF.Brewery.Common.Repositories
{
    public interface IBeerRepository
    {
        Beer? getBeer(int beerId);
        Beer? getBeer(string beerName);
        IQueryable<Beer> getBeers();
        IQueryable<Beer> getBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
        int addBeer(Beer beer);
        int updateBeer(Beer beer);
    }
}
