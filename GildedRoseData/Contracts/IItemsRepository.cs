using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GildedRoseData.Entities;

namespace GildedRoseData.Contracts
{
    public interface IItemsRepository
    {
        Task <Item> GetItem(int Id);

        Task<List<Item>> GetAllItems();

        Task<Item> AddItem(Item itemToAdd);

        Task<Item> UpdateItem(Item updateItem);

    }
}
