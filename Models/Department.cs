// Model for the Department Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thoughtless_eels.Models
{
    public class Department
    {
        // Establish the Primary Key:
        [Key]
        public int DepartmentId { get; set; }

        // Required Property:
        [Required]
        public string Name { get; set; }

        // Required Property:
        [Required]
        public int Budget { get; set; }

    }
}