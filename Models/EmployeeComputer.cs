// Model for the Employee/Computer Joiner Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thoughtless_eels.Models
{
     // Create the Employee/Computer Table: 
    public class EmployeeComputer
    {
        // Establish the Primary Key
        [Key]
        public int EmployeeComputerId { get; set; }
        // First Foreign Key:
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        // Second Foreign Key:
        [Required]
        public int ComputerId { get; set; }
        public Computer Computer { get; set; }

        // Required Property:
        [Required]
        public DateTime StartDate { get; set; }
        // Required Property:
        [Required]
        public DateTime EndDate { get; set; }

    }
}