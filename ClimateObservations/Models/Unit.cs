﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateObservations.Models
{
   public class Unit
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Abbreviation { get; set; }

        public override string ToString()
        {
            string abbrevation = $"{Abbreviation}";
            return abbrevation;
        }
    }
}
