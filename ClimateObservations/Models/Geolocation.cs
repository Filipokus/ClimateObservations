using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservations.Models
{
    class Geolocation
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int AreaId { get; set; }
    }
}
