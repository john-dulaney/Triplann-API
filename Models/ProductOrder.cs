// Model for the ProductOrder Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thoughtless_eels.Models
{
    // Create the ProductOrder Table: 
    public class ProductOrder
    {
        // Establish the Primary Key:
        [Key]
        public int ProductOrderId { get; set; }

        // First Foreign Key:
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Second Foreign Key:
        [Required]
        public int CurrentOrderId { get; set; }
        public CurrentOrder CurrentOrder { get; set; }

    }
}