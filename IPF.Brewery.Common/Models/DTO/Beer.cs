﻿namespace IPF.Brewery.Common.Models.DTO
{
    public class Beer
    {
        public Beer()
        {
            this.Brewery = new HashSet<Brewery>();
            this.Bar = new HashSet<Bar>();
        }

        public int Id { get; set; }
        public string BeerName { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
        public int BeerTypeId { get; set; }
        public virtual BeerType BeerType { get; set; }
        public virtual ICollection<Brewery> Brewery { get; set; }
        public virtual ICollection<Bar> Bar { get; set; }
    }
}