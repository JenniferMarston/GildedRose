using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GildedRoseData.Contracts;
using GildedRoseData.Entities;
using GildedRoseData.Models;
using Microsoft.EntityFrameworkCore;

namespace GildedRoseData.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly GildedRoseItemContext _context;

        public ItemsRepository(GildedRoseItemContext context)
        {
            _context = context;
        }

        public async Task<Item> GetItem(int id )
        {
               return await _context.Item.FindAsync(id);
            
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _context.Item.ToListAsync();
        }

        public async Task<Item> UpdateItem(Item updateItem)
        {
            if (updateItem == null)
            {
                throw new ArgumentException(nameof(updateItem));
            }
            _context.Entry(updateItem).State = EntityState.Modified;
             await _context.SaveChangesAsync();
            return updateItem;
        }

        public async Task<Item> AddItem(Item itemToAdd)
        {
            if (itemToAdd == null)
            {
                throw new ArgumentException(nameof(itemToAdd));
            }
            _context.Item.Add(itemToAdd);
            await _context.SaveChangesAsync();
            return itemToAdd;

        }


    }
}
