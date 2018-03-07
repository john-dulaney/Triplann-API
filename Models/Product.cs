// Model for the Product Table:
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thoughtless_eels.Models
{
     // Create the Product Table: 
    public class Product
    {
        // Establish the Primary Key:
        [Key]
        public int ProductId { get; set; }

        // Required Property:
        [Required]
        public string ProductName { get; set; }

        // Required Property:
        [Required]
        public double Price { get; set; }

        // Required Property:
        [Required]
        public int Quantity { get; set; }

        // Required Property:
        [Required]
        public string Description { get; set; }

        // First Foreign Key:
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Second Foreign Key:
        [Required]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; }

    }
}