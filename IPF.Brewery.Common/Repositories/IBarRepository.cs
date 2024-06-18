using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.Common.Repositories
{
    public interface IBarRepository
    {
        Bar? GetBar(int barId);
        Bar? GetBar(string barName);
        IQueryable<Bar> GetBars();
        Bar? GetBarBeers(int barId);
        IQueryable<Bar> GetAllBarsWithBeers();

        int AddBar(Bar bar);
        int UpdateBar(Bar bar);
    }
}
