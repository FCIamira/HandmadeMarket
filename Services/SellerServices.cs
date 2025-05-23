using System.Security.Claims;
using Azure.Core;
using HandmadeMarket.Interfaces;
using HandmadeMarket.Repository;
using Microsoft.AspNetCore.Http;
using HandmadeMarket.Enum;
using HandmadeMarket.DTO.ProductDTOs;
namespace HandmadeMarket.Services
{
    public class SellerServices
    {
        private readonly ISellerRepo _sellerRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SellerServices(ISellerRepo sellerRepo, IHttpContextAccessor httpContextAccessor)
        {
            _sellerRepo = sellerRepo;
            _httpContextAccessor = httpContextAccessor;
        }


        #region GetAllSellers
        public Result<List<SellerWithProductsDTO>> GetAllSellers()
        {
            var sellers = _sellerRepo.GetAllSellersWithProducts();

            if (sellers == null || !sellers.Any())
            {
                return Result<List<SellerWithProductsDTO>>.Failure(ErrorCode.NotFound,"No sellers found");
            }

            var sellerDtos = sellers.Select(s => new SellerWithProductsDTO
            {
                sellerId = s.UserId,
                storeName = s.storeName,
                email = s.email,
                phoneNumber = s.phoneNumber,
                createdAt = s.createdAt,
                Products = s.Products?.Select(p => new ProductDTO
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                }).ToList() ?? new List<ProductDTO>()
            }).ToList();

            return Result<List<SellerWithProductsDTO>>.Success(sellerDtos);
        }

        #endregion

        #region GetById
        public Result<SellerWithProductsDTO> GetSellerById(string id)
        {
            Seller seller = _sellerRepo.GetSellerWithProductsById(id);
            if (seller == null)
            {
                return Result<SellerWithProductsDTO>.Failure(ErrorCode.NotFound,"Seller not found");
            }
            else
            {
                SellerWithProductsDTO sellerWithProductsDTO = new SellerWithProductsDTO
                {
                    sellerId = seller.UserId,
                    storeName = seller.storeName,
                    email = seller.email,
                    phoneNumber = seller.phoneNumber,
                    createdAt = seller.createdAt,
                    Products = seller.Products?.Select(p => new ProductDTO
                    {
                        ProductId = p.ProductId,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Image = p.Image,
                        Stock = p.Stock,
                    }).ToList() ?? new List<ProductDTO>()
                };
                return Result<SellerWithProductsDTO>.Success(sellerWithProductsDTO);
            }
        }
        #endregion

        #region GetSellerByStoreName
        public Result<SellerWithProductsDTO> GetSellerByStoreName(string storeName)
        {
            Seller seller = _sellerRepo.GetSellerWithProductsByStoreName(storeName);
            if (seller == null)
            {
                return Result<SellerWithProductsDTO>.Failure(ErrorCode.NotFound, "Seller not found");
            }
            else
            {
                SellerWithProductsDTO sellerDTO = new SellerWithProductsDTO
                {
                    sellerId = seller.UserId,
                    storeName = seller.storeName,
                    email = seller.email,
                    phoneNumber = seller.phoneNumber,
                    Products = seller.Products?.Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    }).ToList() ?? new List<ProductDTO>()
                };
                return Result<SellerWithProductsDTO>.Success(sellerDTO);
            }
        }
        #endregion

        #region GetSellerByProductId
        public Result<SellerDTO> GetSellerByProductId(int id)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            Seller seller = _sellerRepo.GetSellerByProductId(id);

            if (seller == null)
            {
                return Result<SellerDTO>.Failure(ErrorCode.NotFound, "Seller not found");
            }
            else
            {
                SellerDTO sellerDTO = new SellerDTO
                {
                    sellerId = userId,
                    storeName = seller.storeName,
                    email = seller.email,
                    phoneNumber = seller.phoneNumber
                };
                return Result<SellerDTO>.Success(sellerDTO);

            }
        }

        #endregion

        #region EditSeller
        public Result<SellerWithProductsDTO> EditSeller(string id, SellerDTO sellerDTO)
        {
            Seller seller = _sellerRepo.GetSellerById(id);
            if (seller == null)
            {
                return Result<SellerWithProductsDTO>.Failure(ErrorCode.NotFound, "Seller not found");
            }
            var Request = _httpContextAccessor.HttpContext?.Request;
            SellerWithProductsDTO sellerWithProductsDTO = new SellerWithProductsDTO
            {
                sellerId = seller.UserId,
                storeName = seller.storeName,
                email = seller.email,
                phoneNumber = seller.phoneNumber,
                createdAt = seller.createdAt,
                Products = seller.Products?.Select(p => new ProductDTO
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = string.IsNullOrEmpty(p.Image) ? null : $"{Request.Scheme}://{Request.Host}{p.Image}",

                }).ToList() ?? new List<ProductDTO>()
            };

            return Result<SellerWithProductsDTO>.Success(sellerWithProductsDTO);
        }

        #endregion
        #region DeleteSeller
        //public Result<SellerWithProductsDTO> DeleteSellerWithProductsById(string id)
        //{
        //    bool deleted = _sellerRepo.DeleteSellerWithProductsById(id);
        //    if (!deleted)
        //        return Result<bool>.Failure(ErrorCode.NotFound, "Seller not found.");

        //    return Result<bool>.Success(deleted);
        //}

        #endregion



    }
}
