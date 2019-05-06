using System;
using System.Collections.Generic;
using System.Text;
using GildedRoseData.Entities;
using Microsoft.EntityFrameworkCore;

namespace GildedRoseData.Models
{
    public class GildedRoseUserContext : DbContext
    {

        public GildedRoseUserContext(DbContextOptions<GildedRoseUserContext> options)
            : base(options)

        {
        }

        public virtual DbSet<GildedRoseData.Entities.GildedRoseUser> GildedRoseUsers { get; set; }

    }

}
