using System.Security.Claims;
using HandmadeMarket.DTO.RatingDTOs;
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
        private readonly IRatingRepo ratingRepo;
        private readonly IProductRepo productRepo;

        public RatingController(IRatingRepo ratingRepo,IProductRepo productRepo)
        {
            this.ratingRepo = ratingRepo;
            this.productRepo = productRepo;
        }


        [HttpGet("{productId}/ratings")]
        public ActionResult<IEnumerable<RatingDTO>> GetRatingsByProductId(int productId)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Check if the product exists
            var product = productRepo.GetById(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            // Get ratings for the specified product
            var ratings = ratingRepo.GetRateingsByProductId(productId)
                .Select(r => new RatingDTO
                {
                    Score = r.Score,
                    Comment = r.Comment,
                    CustomerId = UserId
                })
                .ToList();
            return Ok(ratings);
        }

        // add rating to product
        [HttpPost("{productId}/rate")]
        [Authorize (Roles = "Customer")]
        public IActionResult RateProduct(int productId, [FromBody] AddRateDTO rating)
        {

            // Check if the product exists
            var product =  productRepo.GetById(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;                        
            string userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Check if this customer already rated this product
            var existingRating = ratingRepo.GetAll()
                .FirstOrDefault(r => r.ProductId == productId && r.CustomerId == UserId);

            if (existingRating != null)
            {
                // Update the existing rating
                existingRating.Score = rating.Score;
                existingRating.Comment = rating.Comment;
                ratingRepo.Save();

                return Ok(new { message = "Rating updated successfully." });
            }

            // Add a new rating
            Rating ratingIndatabase = new Rating
            {
                Score = rating.Score,
                Comment = rating.Comment,
                CustomerId = UserId
                ,
                ProductId = productId
            };
           
            ratingRepo.Add(ratingIndatabase);
            ratingRepo.Save();

            return Ok(new { message = "Rating added successfully." });
        }



        [HttpGet("top-rated")]
        public ActionResult<IEnumerable<object>> GetTopRatedProducts()
        {
            var topRated =  productRepo.GetAll()
                .Where(p => p.Ratings != null && p.Ratings.Any())
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.Description,
                    p.Price,
                    AverageRating = p.Ratings.Average(r => r.Score),
                    RatingCount = p.Ratings.Count(),
                   
                })
                .OrderByDescending(p => p.AverageRating)
                .ThenByDescending(p => p.RatingCount)
                .ToList();

            return Ok(topRated);
        }

    }
}
