// Model for the TrainingProgram Table:
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thoughtless_eels.Models
{
     // Create the Training Program Table: 
    public class TrainingProgram
    {
        // Establish the Primary Key:
        [Key]
        public int TrainingProgramId { get; set; }

        // Required Property:
        [Required]
        public string TrainingProgramName { get; set; }

        // Required Property:
        [Required]
        public DateTime StartDate { get; set; }

        // Required Property:
        [Required]
        public string EndDate { get; set; }

        // Required Property:
        [Required]
        public int MaxAttendees { get; set; }

    }
}