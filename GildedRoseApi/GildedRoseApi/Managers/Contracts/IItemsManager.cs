using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRoseApi.Dto;
using GildedRoseData.Entities;

namespace GildedRoseApi.Managers.Contracts
{
    public interface IItemsManager
    {
        Task<PurchaseResponse> PurchaseItem(int itemId, int quantityWanted);
        Task<Item> GetItem(int id);
        Task<List<Item>> GetAllItems();
        Task<Item> AddItem(Item itemToAdd);

    }
}
