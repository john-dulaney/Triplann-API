using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Triplann.Models {
    public class TripType {
        [Key]
        public int TripTypeId { get; set; }

        // Reference for Climate types: http://www.weather-climate.org.uk/13.php

        [Display (Name = "Weather At Destination")]
        public string WeatherTypeId { get; set; }
        public WeatherTypeId WeatherTypeId { get; set; }

        [Display (Name = "Weather At Destination")]
        public string ActivityTypeId { get; set; }
        public ActivityTypeId ActivityTypeId { get; set; }

    }
}