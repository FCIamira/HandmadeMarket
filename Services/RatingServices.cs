using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HandmadeMarket.Services
{
    public class RatingServices
    {
        private readonly IRatingRepo ratingRepo;
        private readonly IProductRepo productRepo;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RatingServices(IRatingRepo ratingRepo,IProductRepo productRepo, IHttpContextAccessor httpContextAccessor)
        {
            this.ratingRepo = ratingRepo;
            this.productRepo = productRepo;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Result<IEnumerable<RatingDTO>> GetRatingsByProductId(int productId)
        {

            var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

           
            // Check if the product exists
            var product = productRepo.GetById(productId);
            if (product == null)
            {
                return Result<IEnumerable<RatingDTO>>.Failure("Product not found.");
            }
            // Get ratings for the specified product
            var ratings = ratingRepo.GetRateingsByProductId(productId)
                .Select(r => new RatingDTO
                {
                    Score = r.Score,
                    Comment = r.Comment,
                    CustomerId = userId
                })
                .ToList();
            return Result<IEnumerable<RatingDTO>>.Success(ratings);
        }

        public Result<string> AddRating(int productId, [FromBody] AddRateDTO rating)
        {
            var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

          

            // Check if the product exists
            var product = productRepo.GetById(productId);
            if (product == null)
            {
                return Result<string>.Failure("Product not found.");
            }
          
            // Check if this customer already rated this product
            var existingRating = ratingRepo.GetAll()
                .FirstOrDefault(r => r.ProductId == productId && r.CustomerId == userId);

            if (existingRating != null)
            {
                // Update the existing rating
                existingRating.Score = rating.Score;
                existingRating.Comment = rating.Comment;
                ratingRepo.Save();

                return Result<string>.Success( "Rating updated successfully." );
            }

            // Add a new rating
            Rating ratingIndatabase = new Rating
            {
                Score = rating.Score,
                Comment = rating.Comment,
                CustomerId = userId
                ,
                ProductId = productId
            };

            ratingRepo.Add(ratingIndatabase);
            ratingRepo.Save();

            return Result<string>.Success("Rating added successfully.");

        }


        public Result<IEnumerable<Object>> GetTopRated()
        {
            var topRated = productRepo.GetAll()
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

            return Result<IEnumerable<Object>>.Success(topRated);
        }

    }
}
