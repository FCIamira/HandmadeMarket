using HandmadeMarket.Enum;
using HandmadeMarket;
using HandmadeMarket.DTO.CategoryDTOs;
using HandmadeMarket.DTO.ProductDTOs;

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
            return Result<List<CategoryWithProductDTO>>.Failure(ErrorCode.NotFound, "No categories found.");
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
            return Result<CategoryWithProductDTO>.Failure(ErrorCode.NotFound, "Category not found");
        }

        var categoryDTO = new CategoryWithProductDTO
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
    #endregion

    #region GetCategoryByName
    public Result<CategoryWithProductDTO> GetCategoryByName(string categoryName)
    {
        Category category = categoryRepo.GetCategoryByName(categoryName);
        if (category == null)
        {
            return Result<CategoryWithProductDTO>.Failure(ErrorCode.NotFound, "Category not found");
        }

        var categoryDTO = new CategoryWithProductDTO
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
    #endregion

    #region AddCategory
    public Result<string> AddCategory(CategoryDTO categoryDto)
    {
        try
        {
            var category = new Category
            {
                name = categoryDto.name,
            };

            categoryRepo.Add(category);
            categoryRepo.Save();

            return Result<string>.Success("Category was created");
        }
        catch (Exception)
        {
            return Result<string>.Failure(ErrorCode.ServerError, "Category not created due to server error");
        }
    }
    #endregion

    #region UpdateCategory
    public Result<string> UpdateCategory(int id, CategoryDTO categoryDTO)
    {
        Category categoryFromDb = categoryRepo.GetById(id);
        if (categoryFromDb == null)
        {
            return Result<string>.Failure(ErrorCode.NotFound, "Category not found");
        }

        categoryFromDb.name = categoryDTO.name;
        categoryRepo.Update(id, categoryFromDb);
        categoryRepo.Save();

        return Result<string>.Success("Category updated");
    }
    #endregion

    #region DeleteCategory
    public Result<string> DeleteCategory(int id)
    {
        Category category = categoryRepo.GetById(id);
        if (category == null)
        {
            return Result<string>.Failure(ErrorCode.NotFound, "Category not found");
        }

        categoryRepo.Remove(id);
        categoryRepo.Save();

        return Result<string>.Success("Category deleted");
    }
    #endregion
}
