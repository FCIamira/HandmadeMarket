

using HandmadeMarket.Enum;
using HandmadeMarket;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HandmadeMarket.DTO.CartDTOs;
using HandmadeMarket.UnitOfWorks;

public class CartServices
{
    private readonly ICartRepo _cartRepo;
    private readonly IUnitOfWork unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartServices(IUnitOfWork unitOfWork ,ICartRepo cartRepo, IHttpContextAccessor httpContextAccessor)
    {
        this.unitOfWork = unitOfWork;
        _cartRepo = cartRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    #region GetAll
    public Result<List<CartGetAll>> GetAll()
    {
        var carts = _cartRepo.GetAll();
        if (carts == null || !carts.Any())
            return Result<List<CartGetAll>>.Failure(ErrorCode.NotFound, "No carts found");

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
        var cart = _cartRepo.GetById(id);
        if (cart == null)
            return Result<Cart>.Failure(ErrorCode.NotFound, "Invalid Id");

        return Result<Cart>.Success(cart);
    }
    #endregion

    #region Add
    public Result<CartWithProductDTO> Add([FromBody] CartWithProductDTO dto)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Result<CartWithProductDTO>.Failure(ErrorCode.Unauthorized, "User not authenticated");

        var cart = new Cart
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            ProductId = dto.ProductId,
            CustomerId = userId
        };

        _cartRepo.Add(cart);
        unitOfWork.SaveChangesAsync();

        return Result<CartWithProductDTO>.Success(dto);
    }
    #endregion

    #region Update
    public Result<string> Update(int id, UpdateCartDTO cartFromReq)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure(ErrorCode.Unauthorized, "User not authenticated");

        var cart = _cartRepo.GetById(id);
        if (cart == null)
            return Result<string>.Failure(ErrorCode.NotFound, "Cart not found");

        cart.Id = cartFromReq.Id;
        cart.Quantity = cartFromReq.Quantity;
        cart.ModifiedBy = userId;

        _cartRepo.Update(id, cart);
        _cartRepo.Save();

        return Result<string>.Success("Cart is updated successfully");
    }
    #endregion

    #region Delete
    public Result<string> Delete(int id)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure(ErrorCode.Unauthorized, "User not authenticated");

        var cart = _cartRepo.GetById(id);
        if (cart == null)
            return Result<string>.Failure(ErrorCode.NotFound, "Cart not found");

        cart.DeletedBy = userId;
        _cartRepo.Remove(id);
        _cartRepo.Save();

        return Result<string>.Success("Cart is deleted successfully");
    }
    #endregion
}
