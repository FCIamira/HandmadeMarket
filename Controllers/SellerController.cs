using HandmadeMarket.Models;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly SellerServices sellerService;

        public SellerController(SellerServices sellerService)
        {
            this.sellerService = sellerService;
        }


        #region GetAllSellers
        [HttpGet]
        public IActionResult GetAllSellersWithProducts()
        {
            var result = sellerService.GetAllSellers();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        #endregion

        #region GetSellerById
        [HttpGet("{id}")]
        public IActionResult GetSellerById(string id)
        {
            var result = sellerService.GetSellerById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        #endregion


        #region GetSellerByStoreName
        [HttpGet("{storeName:alpha}")]
        public IActionResult GetSellerByStoreName(string storeName)
        {
            var result = sellerService.GetSellerByStoreName(storeName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        #endregion


        #region GetSellerByProductId
        [HttpGet("product/{id:int}")]
        public IActionResult GetSellerByProductId(int id)
        {
            var result = sellerService.GetSellerByProductId(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        #endregion

        #region AddSeller
        //
        //[HttpPost]
        //public IActionResult AddSeller(SellerDTO sellerDTO)
        //{
        //    var result = sellerService.AddSeller(sellerDTO);

        //    if (ModelState.IsValid)
        //    {
        //        if (result.IsSuccess)
        //        { return Ok(result); }

        //        else
        //        {
        //            return BadRequest(result);
        //        }
        //    }

        //    return BadRequest(ModelState);

        //}
        //
        #endregion

        #region EditSeller
        [HttpPut("{id}")]
        public IActionResult EditSeller(string id, SellerDTO sellerDTO)
        {
            var result = sellerService.EditSeller(id, sellerDTO);

            if (ModelState.IsValid)
            {
                if (result.IsSuccess)
                { return Ok(result); }

                else
                {
                    return BadRequest(result);
                }
            }

            return BadRequest(ModelState);

        }
        #endregion

        #region DeleteSeller
        //[HttpDelete("{id}")]
        //public IActionResult DeleteSellerWithProductsById(string id)
        //{
        //    var result = sellerService.DeleteSellerWithProductsById(id);

        //    if (!result.IsSuccess)
        //        return NotFound();

        //    return Ok();
        //} 
        #endregion




    }
}

