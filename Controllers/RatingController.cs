using System.Security.Claims;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
       
        private readonly RatingServices ratingServices;

        public RatingController(RatingServices ratingServices)
        {
            this.ratingServices = ratingServices;
        }

        #region GetRatingsByProductId

        [HttpGet("{productId}/ratings")]
        public ActionResult<IEnumerable<RatingDTO>> GetRatingsByProductId(int productId)
        {
            var result = ratingServices.GetRatingsByProductId(productId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        #endregion


        #region AddRateToProduct
        // add rating to product
        [HttpPost("{productId}/rate")]
        [Authorize(Roles = "Customer")]
        public IActionResult RateProduct(int productId, [FromBody] AddRateDTO rating)
        {
            var result = ratingServices.AddRating(productId, rating);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        #endregion


        #region GetTopRatedProducts
        [HttpGet("top-rated")]
        public ActionResult<IEnumerable<object>> GetTopRatedProducts()
        {
            var result = ratingServices.GetTopRated();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        } 
        #endregion

    }
}
