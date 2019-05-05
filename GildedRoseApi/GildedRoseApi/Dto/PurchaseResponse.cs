using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRoseData.Entities;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace GildedRoseApi.Dto
{
    public class PurchaseResponse
    {
        public bool Success { get; }
        public string Message {get;}
        public Item Item { get; }

        public PurchaseResponse(bool success, Item item, string message)
        {
            Success = success;
            Message = message;
            Item = item;
           
        }
    }
}
