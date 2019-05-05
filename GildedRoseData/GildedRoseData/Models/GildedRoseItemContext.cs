using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GildedRoseData.Entities;

namespace GildedRoseData.Models
{
    public partial class GildedRoseItemContext : DbContext
    {
        public GildedRoseItemContext(DbContextOptions<GildedRoseItemContext> options)
            : base(options)
        {
        }

        public DbSet<GildedRoseData.Entities.Item> Item { get; set; }
    }
}
