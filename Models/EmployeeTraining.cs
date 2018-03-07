// Model for the Employee/Training Program Joiner Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thoughtless_eels.Models
{
     // Create the Employee/Training Table: 
    public class EmployeeTraining
    {
        // Establish the Primary Key:
        [Key]

        public int EmployeeTrainingId { get; set; }

        // Required Property:
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        // First Foreign Key:
        [Required]
        public int TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
    }
}