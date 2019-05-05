using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRoseApi.Dto;
using GildedRoseApi.Managers.Contracts;
using GildedRoseData.Contracts;
using GildedRoseData.Entities;
using GildedRoseData.Models;
using GildedRoseData.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GildedRoseApi.Managers
{
    public class ItemsManager: IItemsManager
    {
        private readonly IItemsRepository _itemRepository; //only for crud
        private readonly GildedRoseItemContext _context;

        public ItemsManager(IItemsRepository itemRepository, GildedRoseItemContext context)
        {
            _itemRepository = itemRepository;
            _context = context;
        }

        public async Task<Item> GetItem(int id)
        {
            return await _itemRepository.GetItem(id);
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await  _itemRepository.GetAllItems();
        }


        public async Task<PurchaseResponse> PurchaseItem(int itemId, int quantityWanted)
        {
            if (ItemExists(itemId))
            {
               
                    var theItem = await _context.Item.FindAsync(itemId);
                    if (theItem.QuantityInStock >= quantityWanted)
                    {
                        theItem.QuantityInStock -= quantityWanted;
                        await _itemRepository.UpdateItem(theItem);
                        PurchaseResponse response = new PurchaseResponse(success:true, message :"Successful Purchase",  item: theItem);
                        return response;  //new PurchaseResponse(success: true, item: response.Item);
                    }
                    else
                    {
                        return new PurchaseResponse(success: false, message: "Insufficient Inventory", item: theItem);
                    }

            }
            return new PurchaseResponse(success:false, message :"Invalid Item", item:null);
        }

        public async Task<Item> AddItem(Item itemToAdd)
        {
            if (!ItemExists(itemToAdd.Id))
            {
                await _itemRepository.AddItem(itemToAdd);
                return itemToAdd;
            }

            return null;
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }


    }
}
