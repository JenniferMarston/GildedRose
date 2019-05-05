using System;
using System.Collections.Generic;
using GildedRoseData.Entities;
using GildedRoseData.Models;
using GildedRoseData.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GildedRoseApi.Tests
{
    public class ItemsRepositoryUnitTests : IClassFixture<WebApplicationFactory<GildedRoseApi.Startup>>
    {
        private  GildedRoseItemContext _context;
        private ItemsRepository _itemsRepository;
        private readonly WebApplicationFactory<GildedRoseApi.Startup> _webApplicationFactory;

        public ItemsRepositoryUnitTests(WebApplicationFactory<GildedRoseApi.Startup> factory)
        {
            _webApplicationFactory = factory;
        }

        [Fact]
        public void AddItemAsync_NullParameterThrows()
        {
            _itemsRepository = new ItemsRepository(_context);
            Assert.ThrowsAsync<ArgumentNullException>(() => _itemsRepository.AddItem(null));
        }

        [Fact]
        public void UpdateItemAsync_NullParameterThrows()
        {
            _itemsRepository = new ItemsRepository(_context);
            Assert.ThrowsAsync<ArgumentNullException>(() => _itemsRepository.UpdateItem(null));
        }
        //[Fact]
        //public async void GetAllItemsAsync_DoesNotReturnNull()
        //{
        //    var client =  _webApplicationFactory.CreateClient();
            
        //    List<Item> itemsList = null;
        //    _itemsRepository = new ItemsRepository(_context);
        //    itemsList = await _itemsRepository.GetAllItems();
        //    Assert.NotNull(itemsList);
        //}


    }
}