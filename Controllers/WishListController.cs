using HandmadeMarket.Helpers;
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

    #region GetAll
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _wishListServices.GetAll();
        return result.ToActionResult();
    }
    #endregion

    #region GetById
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _wishListServices.GetById(id);
        return result.ToActionResult();
    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _wishListServices.Delete(id);
        return result.ToActionResult();
    }
    #endregion

    #region Add
    [HttpPost("{id:int}")]
    public IActionResult Add(int id)
    {
        var result = _wishListServices.Add(id);
        return result.ToActionResult();
    }
    #endregion
}
