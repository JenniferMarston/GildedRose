using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GildedRoseData.Entities
{
   public class Item
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public String Name { get; set; }
        [Required]
        [MinLength(5)]
        public String Description { get; set; }
        [Required]
        [Range(1, 99.99)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 1000)]
        public int QuantityInStock { get; set; }
    }
}
