namespace IPF.Brewery.Common.Models.DTO
{
    public class Bar
    {
        public Bar()
        {
            Beer = new HashSet<Beer>();
        }

        public int Id { get; set; }
        public string BarName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Beer> Beer { get; set; }

    }
}
