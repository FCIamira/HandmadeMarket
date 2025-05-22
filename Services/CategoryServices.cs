using HandmadeMarket.Repository;
using HandmadeMarket;
using HandmadeMarket.Interfaces;
using HandmadeMarket.DTO;
using HandmadeMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
namespace HandmadeMarket.Services
{
    public class CategoryServices
    {
        ICategoryRepo categoryRepo;
        public CategoryServices(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }
        public Result<List<CategoryWithProductDTO>> GetAllCategoriesWithProducts()
        {
            IEnumerable<Category> categories = categoryRepo.GetAllCategoriesWithProducts();
            if (categories == null || !categories.Any())
            {
                return Result<List<CategoryWithProductDTO>>.Failure("No categories found.");
            }

            var categoryDTOs = categories.Select(c => new CategoryWithProductDTO
            {
                categoryId = c.categoryId,
                name = c.name,
                Products = c.Products.Select(p => new ProductDTO
                {
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    ProductId = p.ProductId,
                    Stock = p.Stock,
                    Image = p.Image,
                    PriceAfterSale = p.PriceAfterSale > 0 ? p.PriceAfterSale : p.Price,
                    SalePercentage = p.SalePercentage > 0 ? p.SalePercentage : 0,
                }).ToList() ?? new List<ProductDTO>()
            }).ToList();
            return Result<List<CategoryWithProductDTO>>.Success(categoryDTOs);
        }

        public Result<CategoryWithProductDTO> GetById(int id)
        {
            Category category = categoryRepo.GetCategoryDTOById(id);
            if (category == null)
            {
                return Result<CategoryWithProductDTO>.Failure("Not Found");
            }
            else
            {
                CategoryWithProductDTO categoryDTO = new CategoryWithProductDTO
                {
                    categoryId = category.categoryId,
                    name = category.name,
                    Products = category.Products.Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        ProductId = p.ProductId,
                        Stock = p.Stock,
                        Image = p.Image,
                        PriceAfterSale = p.PriceAfterSale > 0 ? p.PriceAfterSale : p.Price,
                        SalePercentage = p.SalePercentage > 0 ? p.SalePercentage : 0,
                    }).ToList() ?? new List<ProductDTO>()
                };
                return Result<CategoryWithProductDTO>.Success(categoryDTO);
            }

        }

        public Result<CategoryWithProductDTO> GetCategoryByName(string CategoryName)
        {
            Category category = categoryRepo.GetCategoryByName(CategoryName);
            if (category == null)
            {
                return Result<CategoryWithProductDTO>.Failure("Not Found");
            }
            else
            {
                CategoryWithProductDTO categoryDTO = new CategoryWithProductDTO
                {
                    categoryId = category.categoryId,
                    name = category.name,
                    Products = category.Products.Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        ProductId = p.ProductId,
                        Stock = p.Stock,
                        Image = p.Image,
                        PriceAfterSale = p.PriceAfterSale > 0 ? p.PriceAfterSale : p.Price,
                        SalePercentage = p.SalePercentage > 0 ? p.SalePercentage : 0,
                    }).ToList() ?? new List<ProductDTO>()
                };
                return Result<CategoryWithProductDTO>.Success(categoryDTO);
            }
        }
    }
}
