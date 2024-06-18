using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.Common.Repositories
{
    public class BeerTypeRepository : IBeerTypeRepository
    {
        private readonly BreweryContext breweryContext;

        public BeerTypeRepository(BreweryContext breweryContext )
        {
            this.breweryContext = breweryContext;
        }

        public IQueryable<BeerType> GetBeerTypes()
        {
            return breweryContext.BeerType.AsQueryable();
        }

        public BeerType? GetBeerType(int beerTypeId)
        {
            return breweryContext.BeerType.FirstOrDefault(b => b.Id == beerTypeId);
        }

        public BeerType? GetBeerType(string beerType)
        {
            return breweryContext.BeerType.FirstOrDefault(b => b.BeerTypeName == beerType);
        }

        public int AddBeerType(BeerType beerType)
        {
            breweryContext.Add(beerType);
            return breweryContext.SaveChanges();
        }

        public int UpdateBeerType(BeerType beerType)
        {
            breweryContext.Update(beerType);
            return breweryContext.SaveChanges();
        }
    }
}