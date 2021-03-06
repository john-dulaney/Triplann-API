using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Triplann.Models {
    public class ChecklistItem {
        [Key]
        public int ChecklistItemId { get; set; }

        [Required]
        public string ChecklistAction { get; set; }

        [Required]
        public int TripTypeId { get; set; }
        public TripType TripType { get; set; }

    }
}