using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GildedRoseData.Entities;
using GildedRoseData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace GildedRoseData.SeedData
{
    public class SeedDb
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            SeedDb.seedUsers(serviceProvider);
            SeedDb.seedItems(serviceProvider);

        }

        private static void seedUsers(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<GildedRoseUserContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<GildedRoseUser>>();
            context.Database.EnsureCreated();

            if (!context.GildedRoseUsers.Any())
            {
                GildedRoseUser user = new GildedRoseUser()
                {
                    Email = "jenna@gildedRose.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "jenna"

                };

                GildedRoseUser adminUser = new GildedRoseUser()
                {
                    Email = "admin@gildedRose.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin"

                };
                userManager.CreateAsync(user, "Jenna123!");
                userManager.CreateAsync(adminUser, "Admin123!");

            }
        }

        private static void seedItems(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<GildedRoseItemContext>();
          //  var userManager = serviceProvider.GetRequiredService<UserManager<GildedRoseItem>>();
            context.Database.EnsureCreated();

            if (!context.Item.Any())
            {
                Item snowGlobe = new Item()
                {
                 Id =1,
                 Name = "Standard Snow Globe",
                 Description = "Your average Snow Globe",
                 Price = 5.99M,
                 QuantityInStock = 20
                };

                Item shipInABottle = new Item()
                {
                    Id =2,
                    Name = "Ship In ABottle",
                    Description = "Queen Ann's Revenge",
                    Price = 7.50M,
                    QuantityInStock = 10
                };


                context.Item.Add(snowGlobe);
                context.Item.Add(shipInABottle);
                context.SaveChangesAsync();
            }
        }






    }
}
