// Model for the Product Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Triplann.Models
{
     // Create the Product Table: 
    public class ChecklistItem
    {
        // Establish the Primary Key:
        [Key]
        public int ChecklistItemId { get; set; }

        // Required Property:
        [Required]
        public string ProductName { get; set; }


        // // First Foreign Key:
        // [Required]
        // public int CustomerId { get; set; }
        // public Customer Customer { get; set; }

    }
}