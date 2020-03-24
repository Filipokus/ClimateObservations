using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservations.Models
{
    public class Observer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<Observation> Observations { get; set; } = new List<Observation>();

        public override string ToString()
        {
            string observer = $"{Firstname} {Lastname}";
            return observer;
        }
    }
}
