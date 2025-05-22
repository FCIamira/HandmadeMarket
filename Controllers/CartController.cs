using HandmadeMarket.Context;
using HandmadeMarket.Interfaces;
using HandmadeMarket.Repository;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CartController : ControllerBase
    {
      private CartServices cartRepo { get; set; }

        public CartController(CartServices cartRepo) { 
            this.cartRepo = cartRepo;
            
        }
        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = cartRepo.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        #endregion

        #region GetById
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = cartRepo.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        #endregion


        //#region MyRegion
        //[HttpPost]
        //public IActionResult Add([FromBody] CartWithProductDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized("User not authenticated");

            //if (!User.IsInRole("Customer"))
            //    return Forbid("Only Customer with the 'user' role can add to the cart.");


        //    var cart = new Cart
        //    {
        //        Id = dto.Id,
        //        Quantity = dto.Quantity,
        //        ProductId = dto.ProductId,
        //        CustomerId = userId
        //    };

        //    cartRepo.Add(cart);
        //    cartRepo.Save();

        //    return CreatedAtAction(nameof(Add), new { id = cart.Id }, dto);
        //}


        //#endregion

        ////////////////////Delete
        //[HttpDelete("{id}")]

        //public IActionResult Delete(int id)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized("User not authenticated");

        //    var cart = cartRepo.GetById(id);
        //    if (cart != null)
        //    {
        //        cart.DeletedBy = userId;
        //        cartRepo.Remove(id);
        //        cartRepo.Save();
        //        return Content("Cart is deleted successfully");
        //    }

        //    return NotFound("Invalid Id");
        //}


        //////////////////////////////////update

        //[HttpPut("{id:int}")]
        //public IActionResult Update(int id, UpdateCartDTO cartFromReq)
        //{
        //    var cart = cartRepo.GetById(id);
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized("User not authenticated");

        //    if (cart != null)
        //    {
        //        cart.Quantity = cartFromReq.Quantity;
        //        cart.ModifiedBy = userId;

        //        cartRepo.Update(id, cart);
        //        cartRepo.Save();
        //        return Content("Cart is updated successfully");
        //    }

        //    return NotFound("Invalid Id");
        //}


    }
}
