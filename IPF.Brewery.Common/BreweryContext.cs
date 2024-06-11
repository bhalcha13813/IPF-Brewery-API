using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common
{
    [ExcludeFromCodeCoverage]
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options)
            : base(options)
        {

        }

        public DbSet<Models.DTO.Brewery> Brewery { get; set; }
        public DbSet<Models.DTO.Bar> Bar { get; set; }
        public DbSet<Models.DTO.Beer> Beer { get; set; }
        public DbSet<Models.DTO.BeerType> BeerType { get; set; }
    }
}
