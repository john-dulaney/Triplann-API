// Model for the Payment Type Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thoughtless_eels.Models
{
     // Create the PaymentType Table: 
    public class PaymentType
    {
        // Establish the Primary Key:
        [Key]
        public int PaymentTypeId { get; set; }

        // Required Property:
        [Required]
        public string Name { get; set; }

        // Required Property:
        [Required]
        public int AccountNumber { get; set; }

        // First Foreign Key:
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}