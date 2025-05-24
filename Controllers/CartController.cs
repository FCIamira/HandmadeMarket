using HandmadeMarket;
using HandmadeMarket.DTO.CartDTOs;
using HandmadeMarket.Enum;
using HandmadeMarket.Helpers;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly CartServices _cartService;

        public CartController(CartServices cartService)
        {
            _cartService = cartService;
        }

        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _cartService.GetAll();
            return result.ToActionResult();
        }
        #endregion

        #region GetById
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _cartService.GetById(id);
            return result.ToActionResult();
        }
        #endregion

        #region Add
        [HttpPost]
        public IActionResult Add([FromBody] CartWithProductDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _cartService.Add(dto);
            return result.ToActionResult();
        }
        #endregion

        #region Update
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCartDTO cartFromReq)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cartService.Update(id, cartFromReq); 
            return result.ToActionResult(); 
        }

        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _cartService.Delete(id);
            return result.ToActionResult();
        }
        #endregion
    }
}


