using Azure.Core;
using HandmadeMarket.DTO.ProductDTOs;
using HandmadeMarket.Enum;
using HandmadeMarket.Interfaces;
using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandmadeMarket.Services
{
    public class ProductServices
    {
        private IProductRepo _productRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductServices(IProductRepo productRepo, IHttpContextAccessor httpContextAccessor)
        {
            _productRepo = productRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        #region GetAllProduct
        public Result<ResponseGetAllProduct> GetAllProduct(int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<Product> products = _productRepo.GetAll();

            if (products == null || !products.Any())
            {
                return Result<ResponseGetAllProduct>.Failure(ErrorCode.NotFound, "product Not Found ");
            }

            int totalCount = products.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            var request = _httpContextAccessor.HttpContext?.Request;

            List<ProductDTO> productDTO = pagedProducts.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Image = string.IsNullOrEmpty(p.Image) ? null : $"{request?.Scheme}://{request?.Host}{p.Image}",

                PriceAfterSale = p.PriceAfterSale > 0 ? p.PriceAfterSale : p.Price,
                SalePercentage = p.SalePercentage > 0 ? p.SalePercentage : 0,
            }).ToList();

            var response = new ResponseGetAllProduct
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Data = productDTO
            };

            return Result<ResponseGetAllProduct>.Success(response);
        }
        #endregion

        #region GetAllProductsHaveSale
        public Result<List<ProductDTO>> GetAllProductsHaveSale()
        {
            IEnumerable<Product> products = _productRepo.GetProductsHaveSale();
            var request = _httpContextAccessor.HttpContext?.Request;

            List<ProductDTO> productDTO = products.Select(products => new ProductDTO
            {
                ProductId = products.ProductId,
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
                Stock = products.Stock,
                PriceAfterSale = products.PriceAfterSale,
                SalePercentage = products.SalePercentage,
                Image = string.IsNullOrEmpty(products.Image) ? null : $"{request?.Scheme}://{request?.Host}{products.Image}",
            }).ToList();

            if (products == null)
            {
                return Result<List<ProductDTO>>.Failure(ErrorCode.NotFound, "Product Not Found");
            }
            return Result<List<ProductDTO>>.Success(productDTO);
        }

        #endregion


        #region GetProductById

        public Result<ProductDTO> GetProductById(int id)
        {
            Product product = _productRepo.GetById(id);
            if (product == null)
            {
                return Result<ProductDTO>.Failure(ErrorCode.NotFound, " Product Not Found");
            }
            else
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                ProductDTO productDTO = new ProductDTO
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    Image = string.IsNullOrEmpty(product.Image) ? null : $"{request?.Scheme}://{request?.Host}{product.Image}",
                    PriceAfterSale = product.PriceAfterSale > 0 ? product.PriceAfterSale : product.Price,
                    SalePercentage = product.SalePercentage > 0 ? product.SalePercentage : 0,
                };

                return Result<ProductDTO>.Success(productDTO);
            }

        }
        #endregion


        #region GetProductByName
        public Result<ProductDTO> GetProductByName(string name)
        {
            Product product = _productRepo.GetProductByName(name);
            if (product == null)
            {
                return Result<ProductDTO>.Failure(ErrorCode.NotFound, "Product not found");
            }
            else
            {
                var request = _httpContextAccessor.HttpContext?.Request;

                ProductDTO productDTO = new ProductDTO
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    Image = string.IsNullOrEmpty(product.Image) ? null : $"{request?.Scheme}://{request?.Host}{product.Image}",

                    PriceAfterSale = product.PriceAfterSale > 0 ? product.PriceAfterSale : product.Price,
                    SalePercentage = product.SalePercentage > 0 ? product.SalePercentage : 0,
                };
                return Result<ProductDTO>.Success(productDTO);
            }

        }
        #endregion


        #region CreateProduct
        public async Task<Result<Product>> CreateProduct([FromForm] AddProductDTO productDTO)
        {
            string imagePath = null;
            if (productDTO.Image != null && productDTO.Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productDTO.Image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDTO.Image.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + uniqueFileName;
            }
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Result<Product>.Failure(ErrorCode.Unauthorized, "Unauthorized: User ID not found.");
            }


            Product product = new Product()
            {
                Description = productDTO.Description,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                Image = imagePath,
                categoryId = productDTO.categoryId,
                sellerId = userId,
                HasSale = productDTO.HasSale,
                SalePercentage = productDTO.SalePercentage,
                PriceAfterSale = _productRepo.CalcPriceAfterSale(productDTO.Price, productDTO.SalePercentage)
            };

            if (product == null)
            {
                return Result<Product>.Failure(ErrorCode.NotFound, "product Not Found ");
            }
            else
            {
                _productRepo.Add(product);
                _productRepo.Save();
                //update image 1
                var request = _httpContextAccessor.HttpContext?.Request;

                Product product1 = _productRepo.GetById(product.ProductId);

                var resultDTO = new ProductDTO
                {
                    ProductId = product1.ProductId,
                    Name = product1.Name,
                    Description = product1.Description,
                    Price = product1.Price,
                    Stock = product1.Stock,
                    //update image 2
                    Image = string.IsNullOrEmpty(product1.Image) ? null : $"{request.Scheme}://{request.Host}{product.Image}",

                    PriceAfterSale = product1.PriceAfterSale,
                    SalePercentage = product1.SalePercentage ?? 0
                };
                return Result<Product>.Success(product);

            }

        }
        #endregion



        #region Edit product

        public Result<ProductDTO> EditProduct(int id, [FromForm] AddProductDTO productDto)
        {


            Product existingProduct = _productRepo.GetProductById(id);
            if (existingProduct == null)
            {
                return Result<ProductDTO>.Failure(ErrorCode.NotFound, "Product Not Found");
            }

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            _productRepo.EditProduct(id, productDto, userId);
            _productRepo.Save();

            Product updatedProduct = _productRepo.GetProductById(id);
            var request = _httpContextAccessor.HttpContext?.Request;

            var productDTO = new ProductDTO
            {
                ProductId = updatedProduct.ProductId,
                CategoryId = updatedProduct.categoryId,
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                Stock = updatedProduct.Stock,
                Image = string.IsNullOrEmpty(updatedProduct.Image) ? null : $"{request.Scheme}://{request.Host}{updatedProduct.Image}",
                SalePercentage = updatedProduct.SalePercentage ?? 0,
                PriceAfterSale = updatedProduct.PriceAfterSale > 0 ? updatedProduct.PriceAfterSale : updatedProduct.Price

            };

            return Result<ProductDTO>.Success(productDTO);
        }

        #endregion

        #region delete product

        public Result<string> DeleteProduct(int id)
        {
            Product product = _productRepo.GetById(id);
            if (product == null)
            {
                return Result<string>.Failure(ErrorCode.NotFound, "Product not found");
            }
            _productRepo.Remove(id);
            _productRepo.Save();
            return Result<string>.Success("Product Deleded");
        }
        #endregion


        #region get top products
        public async Task<Result<IEnumerable<TopProductsDTO>>> GetTopOrderedProductsWithDetails()
        {

            var topProducts = await _productRepo.GetTopProductsByHighestNumberOfOrder();
            if (topProducts == null)
            {
                return Result<IEnumerable<TopProductsDTO>>.Failure(ErrorCode.NotFound, "Product not found");
            }
            return Result<IEnumerable<TopProductsDTO>>.Success(topProducts);
        }
        #endregion


        #region FilterProductsByPrice
        public Result<List<ProductDTO>> FilterProductsByPrice(decimal min, decimal max)
        {
            var products = _productRepo.GetProductsByRanges(min, max);

            if (products == null || !products.Any())
            {
                return Result<List<ProductDTO>>.Failure(ErrorCode.NotFound, "No products found in the specified price range.");
            }

            var request = _httpContextAccessor.HttpContext?.Request;

            var productDTOs = products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Image = string.IsNullOrEmpty(p.Image) ? null : $"{request?.Scheme}://{request?.Host}{p.Image}",
                PriceAfterSale = p.PriceAfterSale > 0 ? p.PriceAfterSale : p.Price,
                SalePercentage = p.SalePercentage > 0 ? p.SalePercentage : 0
            }).ToList();

            return Result<List<ProductDTO>>.Success(productDTOs);
        }

        #endregion
    }
}

