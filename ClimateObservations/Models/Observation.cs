using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservations.Models
{
    public class Observation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            string observation = $"{Id}";
            return observation;
        }
    }
}
