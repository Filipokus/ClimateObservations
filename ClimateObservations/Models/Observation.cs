using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservations.Models
{
    public class Observation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
 /**    public string AreaName { get; set; }
        public int Value { get; set; }
        public string Category { get; set; }
 **/

        public override string ToString()
        {
            string observation = $"{Date.ToString("dd/MM/yyyy")}";
            return observation;
        }
    }
}
