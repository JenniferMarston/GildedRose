using System;
using System.Collections.Generic;
using System.Text;
using GildedRoseData.Entities;
using GildedRoseData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;



namespace GildedRoseApi.Tests
{
   
        public class Fixture : IDisposable
        {
            public GildedRoseUserContext UserContext { get; private set; }
            public GildedRoseItemContext ItemContext { get; private set; }
            public Mock<UserManager<GildedRoseUser>> GrUserManager { get; private set; }
        

            public Fixture()
            {
                this.UserContext = new GildedRoseUserContext(new DbContextOptions<GildedRoseUserContext>());
            
                this.ItemContext  = new GildedRoseItemContext(new DbContextOptions<GildedRoseItemContext>());

                var mockUserStore = new Mock<IUserStore<GildedRoseUser>>();

                GrUserManager = new Mock<UserManager<GildedRoseUser>>(
                    mockUserStore.Object, null, null, null, null, null, null, null, null);

        }

            public void Dispose()
            {
                ItemContext.Dispose();
                ItemContext = null;
                

                UserContext.Dispose();
                UserContext = null;
            }


        }



    }

