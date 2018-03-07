// // Model for the ProductType Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace thoughtless_eels.Models
{
     // Create the ProductType Table: 
    public class ProductType
    {
        // Establish the Primary Key:
        [Key]
        public int ProductTypeId { get; set; }

        // Required Property:
        [Required]
        public string Category { get; set; }

    }
}