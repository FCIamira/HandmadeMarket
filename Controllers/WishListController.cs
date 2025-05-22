using HandmadeMarket.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WishListController : ControllerBase
{
    private readonly WishListServices _wishListServices;

    public WishListController(WishListServices wishListServices)
    {
        _wishListServices = wishListServices;
    }

    //#region GetAll
    //[HttpGet]
    //public IActionResult GetAll()
    //{
    //    var result = _wishListServices.GetAll();
    //    if (result.IsSuccess)
    //    {
    //        return Ok(result.Data);
    //    }
    //    return BadRequest(result.Error);
    //}
    //#endregion

    //[HttpGet("{id}")]
    //public IActionResult GetById(int id)
    //{
    //    WishListDTO wishListFromDB = _wishListServices.GetById(id); // إذا كانت موجودة
    //    if (wishListFromDB == null)
    //        return NotFound("Wishlist item not found");

    //    return Ok(wishListFromDB);
    //}

    //[HttpDelete("{id}")]
    //public IActionResult Delete(int id)
    //{
    //    var result = _wishListServices.Delete(id); // إذا كانت موجودة في الخدمة
    //    if (result.IsSuccess)
    //        return Ok(result.Data);

    //    return NotFound(result.Error);
    //}
}
