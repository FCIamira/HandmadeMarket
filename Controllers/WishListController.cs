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
            WishListDTO wishListFromDB = wishListRepo.GetWishListById(id);

            if (wishListFromDB == null)
                return NotFound("Wishlist item not found");

          

            return Ok(wishListFromDB);
        }
      
        /// /////////////////////////////not completed  add
   
        //[HttpPost]
        //public IActionResult Add(WishListDTO wishList)
        //{
        //    Wishlist wishlist=new Wishlist();

        //    if (ModelState.IsValid)
        //    {
        //        wishListRepo.Add(wishList);
        //        wishListRepo.Save();
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

    }
}
