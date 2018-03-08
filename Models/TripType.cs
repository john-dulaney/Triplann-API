using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Triplann.Models {
    public class TripType {
        [Key]
        public int TripTypeId { get; set; }
        public string WeatherType { get; set; }
        public string TravelMethod { get; set; }
        public string ActivityType { get; set; }
        public string UserId { get; set; }

    }
}