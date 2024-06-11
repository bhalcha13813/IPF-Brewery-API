namespace IPF.Brewery.Common.Models.DTO
{
    public class Brewery
    {
        public Brewery()
        {
            this.Beer = new HashSet<Beer>();
        }

        public int Id { get; set; }
        public string BreweryName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Beer> Beer { get; set; }

    }
}
