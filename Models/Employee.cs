// // Model for the Employee Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace thoughtless_eels.Models
{
    // Create the Employee Table: 
    // Establish the Primary Key as well the Employee's properties: 
    public class Employee
    {
        // Establish the Primary Key:
        [Key]
        public int EmployeeId { get; set; }

        // Required Property:
        [Required]
        public string Name { get; set; }

        // Required Property:
        [Required]
        public int Supervisor { get; set; }

        // First Foreign Key:
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}