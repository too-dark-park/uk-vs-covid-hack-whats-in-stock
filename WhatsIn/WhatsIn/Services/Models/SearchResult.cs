using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsIn.Services.Models
{
    public class SearchResult
    {
        public string PlaceName { get; set; }
        public string ProductName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
