using System;
using GildedRoseApi.Managers;
using GildedRoseApi.Managers.Contracts;
using GildedRoseData.Entities;
using GildedRoseData.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;


namespace GildedRoseApi.Tests
{
    public class AuthenticationTests : IClassFixture<Fixture>
    {

        private Fixture _fixture;
        

        public AuthenticationTests(Fixture fixture )
        {

            _fixture = fixture;
          
        }

        [Fact]
        public void TestLoginSuccess()
        {


        }
    }
}
