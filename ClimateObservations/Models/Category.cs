using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservations.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseCategoryId { get; set; }
        public int UnitId { get; set; }

        public override string ToString()
        {
            string category = $"{Name}";
            return category;
        }
    }
}
