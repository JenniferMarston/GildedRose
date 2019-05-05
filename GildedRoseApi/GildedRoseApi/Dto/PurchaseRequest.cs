using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GildedRoseData.Entities
{
    public class PurchaseRequest
    {
        [Required]
        public int  ItemId { get; set; }
        public int QuantityWanted { get; set; }


    }
}
