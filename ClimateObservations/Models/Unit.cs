using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservations.Models
{
    class Unit
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Abbreviation { get; set; }

        public override string ToString()
        {
            string category = $"{Abbreviation}";
            return category;
        }
    }
}
