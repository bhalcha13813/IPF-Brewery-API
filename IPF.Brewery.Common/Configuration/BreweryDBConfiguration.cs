using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPF.Brewery.Common.Configuration
{
    public class BreweryDBConfiguration
    {
        public string ConnectionString { get; set; }

        public int MaxRetryCount { get; set; }

        public int MaxRetryDelayInSeconds { get; set; }
    }
}
