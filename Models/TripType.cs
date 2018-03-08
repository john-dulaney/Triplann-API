using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Triplann.Models
{
    public class TripType
    {
        [Key]
        public int TripTypeId { get; set; }


        // Reference for Climate types: http://www.weather-climate.org.uk/13.php
        [Display(Name = "Very Cold Polar")]
        public string PolarVeryCold { get; set; }

        
        [Display(Name = "Mildly Cold Polar")]
        public string PolarMildCold { get; set; }

        
        [Display(Name = "Very Hot Arid")]
        public string AridVeryHot { get; set; }

        
        [Display(Name = "Moderate to cold Arid")]
        public string AridCold { get; set; }

        
        [Display(Name = "Moderate Tropical (Winter)")]
        public string TropicalModerate { get; set; }

        
        [Display(Name = "Very Warm Tropical")]
        public string TropicalVeryHot { get; set; }

        
        [Display(Name = "Very Hot Temperate")]
        public string TemperateVeryHot { get; set; }

        
        [Display(Name = "Moderate to cold Temperate")]
        public string TemperateCold { get; set; }
        

        // [Display(Name = "x")]
        // public string x { get; set; }


        
        public int AccountNumber { get; set; }

        // [Required]
        // public int CustomerId { get; set; }
        // public Customer Customer { get; set; }
    }
}