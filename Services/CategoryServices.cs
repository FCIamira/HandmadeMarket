using HandmadeMarket.Enum;
using HandmadeMarket;
using HandmadeMarket.DTO.CategoryDTOs;
using HandmadeMarket.DTO.ProductDTOs;
using HandmadeMarket.UnitOfWorks;

public class CategoryServices
{
    public IUnitOfWork UnitOfWork { get; }
    private readonly IHttpContextAccessor _httpContextAccessor;


    public CategoryServices(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        UnitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    #region GetAllCategoriesWithProducts
    public Result<List<CategoryWithProductDTO>> GetAllCategoriesWithProducts()
    {
        IEnumerable<Category> categories = UnitOfWork.Category.GetAllCategoriesWithProducts();
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
        Category category = UnitOfWork.Category.GetCategoryDTOById(id);
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
        Category category = UnitOfWork.Category.GetCategoryByName(categoryName);
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

            UnitOfWork.Category.Add(category);
            UnitOfWork.SaveChangesAsync();

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
        Category categoryFromDb = UnitOfWork.Category.GetById(id);
        if (categoryFromDb == null)
        {
            return Result<string>.Failure(ErrorCode.NotFound, "Category not found");
        }

        categoryFromDb.name = categoryDTO.name;
        UnitOfWork.Category.Update(id, categoryFromDb);
        UnitOfWork.SaveChangesAsync();

        return Result<string>.Success("Category updated");
    }
    #endregion

    #region DeleteCategory
    public Result<string> DeleteCategory(int id)
    {
        Category category = UnitOfWork.Category.GetById(id);
        if (category == null)
        {
            return Result<string>.Failure(ErrorCode.NotFound, "Category not found");
        }

        UnitOfWork.Category.Remove(id);
        UnitOfWork.SaveChangesAsync();

        return Result<string>.Success("Category deleted");
    }
    #endregion
}
