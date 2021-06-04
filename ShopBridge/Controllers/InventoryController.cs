using Microsoft.AspNetCore.Mvc;
using ShopBridge.Common;
using ShopBridge.ViewModel;
using System;
using System.Linq;

namespace ShopBridge.Controllers
{
    public class InventoryController : BaseController
    {
        [HttpGet("Get")]
        public IActionResult Get()
        {
            try
            {
                var inventory = db.Inventory.OrderByDescending(x => x.ModifiedOn);
                return Ok(ReturnResult.SuccessResponse(inventory, "Found inventory data."));
            }
            catch (Exception ex)
            {
                Log.ErrorMsg(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] int id)
        {
            try
            {
                var inventory = db.Inventory.Where(c => c.InventoryId == id);
                if (inventory.Count() > 0)
                {
                    return Ok(ReturnResult.SuccessResponse(inventory, "Found inventory data."));
                }
                return Ok(ReturnResult.FailureResponse("Record with id ([Id]) not found.".Replace("[Id]", id.ToString())));
            }
            catch (Exception ex)
            {
                Log.ErrorMsg(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Post")]
        public IActionResult Post([FromBody] Inventory inventory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(ReturnResult.FailureResponse("Enter a valid information."));
                }

                if (inventory.Price > 0 && !String.IsNullOrEmpty(inventory.Name))
                {
                    var inventorys = db.Inventory.Where(c => c.Name == inventory.Name && c.Description == inventory.Description && c.Price == inventory.Price);
                    if (inventorys.Count() == 0)
                    {
                        db.Inventory.Add(inventory);
                        db.SaveChanges();
                        return Ok(ReturnResult.SuccessResponse("Record inserted."));
                    }
                    return Ok(ReturnResult.FailureResponse("This inventory ([Name]) already exists.".Replace("[Name]", inventory.Name)));
                }
                return Ok(ReturnResult.FailureResponse("Enter a valid information."));
            }
            catch (Exception ex)
            {
                Log.ErrorMsg(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Put")]
        public IActionResult Put([FromBody] Inventory inventory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(ReturnResult.FailureResponse("Enter a valid information."));
                }
                if (inventory.Price > 0 && !String.IsNullOrEmpty(inventory.Name) && inventory.InventoryId > 0)
                {
                    var inventorys = db.Inventory.Where(c => c.InventoryId == inventory.InventoryId).FirstOrDefault();
                    if (inventorys != null)
                    {
                        inventorys.Name = inventory.Name;
                        inventorys.Description = inventory.Description;
                        inventorys.Price = inventory.Price;
                        inventorys.ModifiedBy = inventory.ModifiedBy;
                        inventorys.IPAddress = inventory.IPAddress;
                        db.SaveChanges();
                        return Ok(ReturnResult.SuccessResponse("Record updated."));
                    }
                    return Ok(ReturnResult.FailureResponse("Record with id ([Id]) not found.".Replace("[Id]", inventory.InventoryId.ToString())));
                }
                return Ok(ReturnResult.FailureResponse("Enter a valid information."));
            }
            catch (Exception ex)
            {
                Log.ErrorMsg(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                if (id > 0)
                {
                    var inventory = db.Inventory.Where(c => c.InventoryId == id).FirstOrDefault();
                    if (inventory != null)
                    {
                        db.Inventory.Remove(inventory);
                        db.SaveChanges();
                        return Ok(ReturnResult.SuccessResponse("Record deleted."));
                    }
                }
                return Ok(ReturnResult.FailureResponse("Record with id ([Id]) not found.".Replace("[Id]", id.ToString())));
            }
            catch (Exception ex)
            {
                Log.ErrorMsg(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
