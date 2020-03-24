using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservations.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int ObservationId { get; set; }
        public int CategoryId { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
