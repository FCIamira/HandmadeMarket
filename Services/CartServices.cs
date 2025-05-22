using Azure.Core;
using HandmadeMarket.Interfaces;
using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using Microsoft.AspNetCore.Mvc;

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

    }
}
