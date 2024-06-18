using IPF.Brewery.Common.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace IPF.Brewery.Common.Repositories
{
    public class BarRepository : IBarRepository
    {
        private readonly BreweryContext breweryContext;
        public BarRepository(BreweryContext breweryContext )
        {
            this.breweryContext = breweryContext;
        }

        public Bar? GetBar(int barId)
        {
            return breweryContext.Bar.FirstOrDefault(b => b.Id == barId);
        }

        public Bar? GetBar(string barName)
        {
            return breweryContext.Bar.FirstOrDefault(b => b.BarName== barName);
        }

        public IQueryable<Bar> GetBars()
        {
            return breweryContext.Bar.AsQueryable();
        }

        public Bar? GetBarBeers(int barId)
        {
            return breweryContext.Bar
                                 .Include(b => b.Beer)
                                 .Include("Beer.BeerType")
                                 .FirstOrDefault(b => b.Id == barId);
        }

        public IQueryable<Bar> GetAllBarsWithBeers()
        {
            return breweryContext.Bar
                                 .Include(b => b.Beer)
                                 .Include("Beer.BeerType")
                                 .AsQueryable();
        }

        public int AddBar(Bar bar)
        {
            breweryContext.Add(bar);
            return breweryContext.SaveChanges();
        }

        public int UpdateBar(Bar bar)
        {
            breweryContext.Update(bar);
            return breweryContext.SaveChanges();
        }
    }
}