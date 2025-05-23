using HandmadeMarket.DTO;
using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using Microsoft.AspNetCore.Http;

namespace HandmadeMarket.Services
{
    public class WishListServices
    {
        private readonly IWishList _wishListRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WishListServices(IWishList wishListRepo, IHttpContextAccessor httpContextAccessor)
        {
            _wishListRepo = wishListRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        #region GetAll
        public Result<IEnumerable<WishListDTO>> GetAll()
        {
            var wishlists = _wishListRepo.GetWishLists();

            if (wishlists == null || !wishlists.Any())
            {
                return Result<IEnumerable<WishListDTO>>.Failure("Wishlist is empty");
            }

            var request = _httpContextAccessor.HttpContext?.Request;

            var wishlistDTOs = wishlists.Select(w => new WishListDTO
            {
                Id = w.Id,
                ProductId = w.ProductId,
                ProductName = w.Product?.Name,
                ProductDescription = w.Product?.Description,
                ProductPrice = w.Product?.Price ?? 0,
                Image = string.IsNullOrEmpty(w.Product?.Image) ? null : $"{request?.Scheme}://{request?.Host}{w.Product.Image}"
            });

            return Result<IEnumerable<WishListDTO>>.Success(wishlistDTOs);
        }

        #endregion

        #region GetById
        public Result<WishListDTO> GetById(int id)
        {
            var wishlist = _wishListRepo.GetWishListById(id);
            if (wishlist == null)
                return Result<WishListDTO>.Failure("Wishlist item not found");

            var request = _httpContextAccessor.HttpContext?.Request;

            var dto = new WishListDTO
            {
                Id = wishlist.Id,
                ProductId = wishlist.ProductId,
                ProductName = wishlist.Product?.Name,
                ProductDescription = wishlist.Product?.Description,
                ProductPrice = wishlist.Product?.Price ?? 0,
                Image = string.IsNullOrEmpty(wishlist.Product?.Image)
                    ? null
                    : $"{request?.Scheme}://{request?.Host}{wishlist.Product.Image}"
            };

            return Result<WishListDTO>.Success(dto);
        }

        #endregion

        #region Add
        public Result<string> Add(WishListDTO dto)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Result<string>.Failure("User not authenticated.");
            }

            if (_wishListRepo.Exists(dto.ProductId, userId))
            {
                return Result<string>.Failure("This product is already in your wishlist.");
            }

            var wishlist = new Wishlist
            {
                ProductId = dto.ProductId,
                CustomerId = userId
            };

            _wishListRepo.Add(wishlist);
            _wishListRepo.Save();

            return Result<string>.Success("Item added to wishlist successfully");
        }
        #endregion


        #region Delete
        public Result<string> Delete(int id)
        {
            var wishlist = _wishListRepo.GetById(id);
            if (wishlist == null)
                return Result<string>.Failure("Wishlist item not found");

            _wishListRepo.Remove(id);
            _wishListRepo.Save();

            return Result<string>.Success("Wishlist item deleted successfully");
        }
        #endregion


    }
}

