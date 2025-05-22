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
        #region GetAllCategoriesWithProducts
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

        #endregion

        #region GetById
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

        #endregion

        #region GetCategoryByName
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

        #endregion

        #region AddCategory
        public Result<string> AddCategory(CategoryDTO categoryDto)
        {
            try
            {
                Category category = new Category
                {
                    name = categoryDto.name,
                };

                categoryRepo.Add(category);
                categoryRepo.Save();

                return Result<string>.Success("Category was Created");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure("Category Not Created");
            }
        }

        #endregion


        #region UpdateCategory
        public Result<string> UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            Category categoryFromdb = categoryRepo.GetById(id);
            if (categoryDTO == null)
            {
                return Result<string>.Failure("Category not Found");
            }

            categoryFromdb.name = categoryDTO.name;
            categoryRepo.Update(id, categoryFromdb);
            categoryRepo.Save();
            return Result<string>.Success("Category Update");
        }

        #endregion

        #region DeleteCategory
        public Result<string> DeleteCategory(int id)
        {
            Category category = categoryRepo.GetById(id);
            if (category == null)
            {
                return Result<string>.Failure($"{nameof(Category)} not found");
            }
            categoryRepo.Remove(id);
            categoryRepo.Save();
            return Result<string>.Success("Category Deleted");
        }

        #endregion


    }
}
