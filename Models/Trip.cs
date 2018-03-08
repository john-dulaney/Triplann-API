using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Triplann.Models {
    public class Trip {
        [Key]
        public int TripId { get; set; }

        [Required]
        public string Location { get; set; }
        
        [Required]
        public string Duration { get; set; }

        [Required]
        public int TripTypeId { get; set; }
        public TripType TripType { get; set; }

        [Required]
        public int ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}