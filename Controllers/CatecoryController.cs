using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatecoryController : ControllerBase
    {
        ICategoryRepo categoryRepo;
        public CatecoryController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Category> categories = categoryRepo.GetAllCategoriesWithProducts();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
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

            return Ok(categoryDTOs);
        }

        //[HttpGet("{id:int}")]
        //public IActionResult GetCatecoryById(int id)
        //{
        //    Category category = categoryRepo.GetById(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(category);
        //}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Category category = categoryRepo.GetCategoryDTOById(id);
            if (category == null)
            {
                return NotFound();
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
                return Ok(categoryDTO);
            }
        }
        [HttpGet("{CategoryName:alpha}")]
        public IActionResult GetCategoryByName(string CategoryName)
        {
            Category category = categoryRepo.GetCategoryByName(CategoryName);
            if (category == null)
            {
                return NotFound();
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
                return Ok(categoryDTO);
            }
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryDTO categoryDto)
        {
            Category category = new Category
            {
                name = categoryDto.name,
            };
            if (ModelState.IsValid)
            {
                categoryRepo.Add(category);
                categoryRepo.Save();

                return CreatedAtAction("GetById", new { id = categoryDto.categoryId }, categoryDto);
            }

            return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult UpdateCategory(int id ,CategoryDTO categoryDTO)
        {
            Category categoryFromdb = categoryRepo.GetById(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }
            categoryFromdb.name = categoryDTO.name;
            categoryRepo.Update(id,categoryFromdb);
            categoryRepo.Save();
            return Ok(categoryFromdb);
        } 
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            Category category = categoryRepo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            categoryRepo.Remove(id);
            categoryRepo.Save();
            return Ok(category);
        }
    }
}
