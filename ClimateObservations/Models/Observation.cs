using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservations.Models
{
    public class Observation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ObserverId { get; set; }
        public int GeolocationId { get; set; }
        public List<Measurement> Measurements { get; set; } = new List<Measurement>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Area> Areas { get; set; } = new List<Area>();


        public override string ToString()
        {
            List<string> areas = new List<string>();
            List<string> categories = new List<string>();
            foreach (var a in Areas)
            {
                areas.Add(a.Name);
            }
            foreach (var c in Categories)
            {
                categories.Add(c.Name);
            }
            string areasString = string.Join(",", areas.ToArray());
            string categoriesString = string.Join(",", categories.ToArray());
            string observation = $"{Date.ToString("dd/MM/yyyy")}";
            return $"{observation}, {areasString} - {categoriesString}";
        }
    }
}
