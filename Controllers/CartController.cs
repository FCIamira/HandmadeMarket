using HandmadeMarket.Context;
using HandmadeMarket.DTO;
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


        #region MyRegion
        [HttpPost]
        public IActionResult Add([FromBody] CartWithProductDTO dto)
        {


            var result =  cartRepo.Add(dto);
            if (ModelState.IsValid)
            {
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }


        #endregion

        #region Delete
        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {

            var result = cartRepo.Delete(id);
            if (ModelState.IsValid)
            {
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Update
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, UpdateCartDTO cartFromReq)
        {
            var result = cartRepo.Delete(id);
            if (ModelState.IsValid)
            {
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }


        #endregion

    }
}
