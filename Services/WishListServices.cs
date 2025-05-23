using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using Microsoft.AspNetCore.Http;
using HandmadeMarket.Enum;
using HandmadeMarket.UnitOfWorks;
using System.Security.Claims;
namespace HandmadeMarket.Services
{
    public class WishListServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WishListServices(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        #region GetAll
        public Result<IEnumerable<WishListDTO>> GetAll()
        {
            var wishlists = unitOfWork.WishList.GetWishLists();

            if (wishlists == null || !wishlists.Any())
            {
                return Result<IEnumerable<WishListDTO>>.Failure(ErrorCode.NotFound,"Wishlist is empty");
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
            var wishlist = unitOfWork.WishList.GetWishListById(id);
            if (wishlist == null)
                return Result<WishListDTO>.Failure(ErrorCode.NotFound, "Wishlist item not found");

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
        public Result<string> Add(int id)///check her if product is exsit or not
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Result<string>.Failure(ErrorCode.NotFound, "User not authenticated.");
            }

            if (unitOfWork.WishList.Exists(id, userId))
            {
                return Result<string>.Failure(ErrorCode.NotFound, "This product is already in your wishlist.");
            }

            var wishlist = new Wishlist
            {
                ProductId = id,
                CustomerId = userId
            };

            unitOfWork.WishList.Add(wishlist);
            unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Item added to wishlist successfully");
        }
        #endregion


        #region Delete
        public Result<string> Delete(int id)
        {
            var wishlist = unitOfWork.WishList.GetById(id);
            if (wishlist == null)
                return Result<string>.Failure(ErrorCode.NotFound, "Wishlist item not found");

            unitOfWork.WishList.Remove(id);
            unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Wishlist item deleted successfully");
        }
        #endregion


    }
}

