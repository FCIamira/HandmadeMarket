using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {

        private IWishList wishListRepo;
        public WishListController(IWishList wishListRepo)
        {
            this.wishListRepo = wishListRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
           List<WishListDTO> wishListes = wishListRepo.GetWishLists();
            if (wishListes != null)
            {

                return Ok(wishListes);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Wishlist wishListFromDB = wishListRepo.GetById(id);

            if (wishListFromDB == null)
                return NotFound("Wishlist item not found");

            if (wishListFromDB.Product == null)
                return BadRequest("Product data is missing in the wishlist item");

            WishListDTO dto = new WishListDTO
            {
                Id = wishListFromDB.Id,
                ProductDescription = wishListFromDB.Product.Description,
                ProductName = wishListFromDB.Product.Name,
                ProductId = wishListFromDB.Product.ProductId,
                Image = wishListFromDB.Product.Image,
            };

            return Ok(dto);
        }

        //[HttpPost]
        //public IActionResult Add(Cart cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        cartRepo.Add(cart);
        //        cartRepo.Save();
        //        return Created();
        //    }
        //    return BadRequest();
        //}

        //////////////////Delete
        [HttpDelete]
        public IActionResult Delete(int id)
        {
          Wishlist wishlist= wishListRepo.GetById(id);
            if (wishlist != null)
            {
                wishListRepo.Remove(id);
                wishListRepo.Save();
                return Content("WishList is deleted sucessfully");
            }
            return NotFound("Invalid Id");
        }

        //////////////////////////////////update

        //[HttpPut("{id:int}")]
        //public IActionResult Update(int id, Cart cart)
        //{
        //    Cart cart1 = cartRepo.GetById(id);
        //    if (cart1 != null)
        //    {
        //        cartRepo.Update(id, cart);
        //        cartRepo.Save();
        //        return Content("Cart is updated sucessfully");
        //    }
        //    return NotFound("Invalid Id");

        //}
    }
}
