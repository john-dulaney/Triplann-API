// Model for the Customer Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thoughtless_eels.Models
{
    // Create the Customer Table: 
    // Establish the Primary Key as well it's Properties: 
    
    public class Customer
    {
        // Establish the Primary Key:
        [Key]
        public int CustomerId { get; set; }

        // Required Property:
        [Required]
        public string FirstName { get; set; }

        // Required Property:
        [Required]
        public string LastName { get; set; }

        // Required Property:
        [Required]
        public string CreatedOn { get; set; }

        // Required Property:
        [Required]
        public int DaysInactive { get; set; }
    }
}