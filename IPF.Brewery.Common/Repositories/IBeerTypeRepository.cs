using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.Common.Repositories
{
    public interface IBeerTypeRepository
    {
        IQueryable<BeerType> GetBeerTypes();
        BeerType? GetBeerType(int beerTypeId);
        BeerType? GetBeerType(string beerType);
        int AddBeerType(BeerType beerType);
        int UpdateBeerType(BeerType beerType);
    }
}
