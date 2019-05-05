using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRoseApi.Managers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GildedRoseData.Entities;
using GildedRoseData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace GildedRoseApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
    
        private readonly IItemsManager _manager;

        public ItemsController(IItemsManager manager, GildedRoseItemContext context)
        {
            _manager = manager;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<IActionResult> GetItem()
        {
            List<Item> items = await _manager.GetAllItems();
            return (Ok(items));
        }

        // PUT: api/Items/Purchase/1
        [HttpPut()]
        [Route("purchase/{id}")]
        public async Task<IActionResult> PutItem([FromRoute] int id, [FromBody] PurchaseRequest purchaseRequest)
        {

            /***Please note, Generally we would create a purchase controller, and which would 
            Post to the purchase entity after debiting the Item's quantity.  For example purposes, here we just update the item and debit the item quantity ***/

            if (ModelState.IsValid && id == purchaseRequest.ItemId)
            {
                try
                {
                    var purchaseResult = _manager.PurchaseItem(purchaseRequest.ItemId, purchaseRequest.QuantityWanted);
                    return Ok(purchaseResult.Result);
                }
                catch (Exception ex)
                {
                    throw  ex;
                }
            }
            return BadRequest();
        }

        // POST: api/Items
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> PostItem([FromBody] Item item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var createResult = await _manager.AddItem(item);
                    if (createResult != null)
                    {
                       return CreatedAtAction("GetItem", new {id = item.Name}, item);
                    }
                    else
                    {
                        return BadRequest(new {message = "Item already Found/Insert Error"});
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(UnauthorizedResult))
                    {
                        return Unauthorized();
                    }
                    return BadRequest(new {Success = false, Error = ex});
                }
            }
                return BadRequest(ModelState);
        }


        // GET: api/Items/1
        [HttpGet("{id?}")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _manager.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}