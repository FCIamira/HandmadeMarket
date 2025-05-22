using Azure.Core;
using HandmadeMarket.Interfaces;
using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandmadeMarket.Services
{
    public class CartServices
    {
        private ICartRepo _cartRepo;
        private IHttpContextAccessor _httpContextAccessor;

        public CartServices(ICartRepo cartRepo, IHttpContextAccessor httpContextAccessor) 
        {
            _cartRepo= cartRepo;
            _httpContextAccessor= httpContextAccessor;
        }


        #region GetAll
        [HttpGet]
        public Result<List<CartGetAll>> GetAll()
        {
            IEnumerable<Cart> carts = _cartRepo.GetAll();

            if (carts == null || !carts.Any())
            {
                return Result<List<CartGetAll>>.Failure("No carts found");
            }
            var request = _httpContextAccessor.HttpContext?.Request;

            var cartsDto = carts.Select(c => new CartGetAll
            {
                Id = c.Id,
                ProductId = c.ProductId,
                ProductName = c.Product.Name,
                Quantity = c.Quantity,
                Price = c.Product.Price,
                Stock = c.Product.Stock,
                Image = string.IsNullOrEmpty(c.Product.Image) ? null : $"{request.Scheme}://{request.Host}{c.Product.Image}",
            }).ToList();

            return Result<List<CartGetAll>>.Success(cartsDto);
        }

        #endregion

        #region GetById
        public Result<Cart> GetById(int id)
        {
            Cart cart = _cartRepo.GetById(id);
            if (cart != null)
            {

                return Result<Cart>.Success(cart);
            }
            return Result<Cart>.Failure("Invalid Id");
        }
        #endregion

        #region Add
        public Result<CartWithProductDTO> Add([FromBody] CartWithProductDTO dto)
        {

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Result<CartWithProductDTO>.Failure("User not authenticated");

         

            var cart = new Cart
            {
                Id = dto.Id,
                Quantity = dto.Quantity,
                ProductId = dto.ProductId,
                CustomerId = userId
            };

            _cartRepo.Add(cart);
            _cartRepo.Save();

            return Result<CartWithProductDTO>.Success(dto);
        }


        #endregion

        #region Update

       
        public Result<string> Update(int id, UpdateCartDTO cartFromReq)
        {
            var cart = _cartRepo.GetById(id);
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Result<string>.Failure("User not authenticated");

            if (cart != null)
            {
                cart.Id = cartFromReq.Id;
                cart.Quantity = cartFromReq.Quantity;
                cart.ModifiedBy = userId;

                _cartRepo.Update(id, cart);
                _cartRepo.Save();
                return Result<string>.Success("Cart is updated successfully");
            }

            return Result<string>.Failure("Invalid Id");
        }
        #endregion

        #region Delete
        public Result<string> Delete(int id)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Result<string>.Failure("User not authenticated");

            var cart = _cartRepo.GetById(id);
            if (cart != null)
            {
                cart.DeletedBy = userId;
                _cartRepo.Remove(id);
                _cartRepo.Save();
                return Result<string>.Success("Cart is deleted successfully");
            }

            return Result<string>.Failure("Invalid Id");

        }

        #endregion
    }
}
