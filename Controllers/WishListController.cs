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
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Error);
    }
    #endregion

    #region MyRegion
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _wishListServices.GetById(id);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Error);
    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _wishListServices.Delete(id); 
        if (result.IsSuccess)
            return Ok(result.Data);

        return NotFound(result.Error);
    }
    #endregion

    #region Add
    [HttpPost]
    public IActionResult Add([FromBody] WishListDTO dto)
    {
        var result = _wishListServices.Add(dto);
        if (result.IsSuccess)
            return Ok(result.Data);

        return BadRequest(result.Error);
    } 
    #endregion


}
