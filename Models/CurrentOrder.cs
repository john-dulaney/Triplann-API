// Model for the CurrentOrder Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thoughtless_eels.Models
{
    // Create the CurrentOrder Table: 
    // Establish the Primary Key as well the 2 Foreign Keys: 
    public class CurrentOrder
    {
        // Establish the Primary Key:
        [Key]
        public int CurrentOrderId { get; set; }

        // First Foreign Key:
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Second Foreign Key:
        [Required]
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        
        //  
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}