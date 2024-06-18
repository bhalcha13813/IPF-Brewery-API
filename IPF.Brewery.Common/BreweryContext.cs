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

        public Microsoft.EntityFrameworkCore.DbSet<Models.DTO.Brewery> Brewery { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Models.DTO.Bar> Bar { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Models.DTO.Beer> Beer { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Models.DTO.BeerType> BeerType { get; set; }
    }
}
