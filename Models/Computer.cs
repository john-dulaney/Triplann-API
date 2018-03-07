// Model for the Computer Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace thoughtless_eels.Models
{
    // Create the Computer Table: 
    // Establish the Primary Key as well the Computer's properties: 
    public class Computer
    {
        // Establish the Primary Key:
        [Key]
        public int ComputerId { get; set; }

        // Required Property:
        [Required]
        public string Name { get; set; }

        // Required Property:
        [Required]
        public string PurchasedOn { get; set; }

        // Required Property:
        public string DecomissionedOn { get; set; }

        // Required Property:
        [Required]
        public int Malfunction { get; set; }

        // Required Property:
        [Required]
        public int Available { get; set; }

    }
}