using HandmadeMarket.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatecoryController : ControllerBase
    {
        ICategoryRepo categoryRepo;
        CategoryServices categoryServices;
        public CatecoryController(ICategoryRepo categoryRepo, CategoryServices categoryServices)
        {
            this.categoryRepo = categoryRepo;
            this.categoryServices = categoryServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            Result<List<CategoryWithProductDTO>> result = categoryServices.GetAllCategoriesWithProducts();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Result<CategoryWithProductDTO> result = categoryServices.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{CategoryName:alpha}")]
        public IActionResult GetCategoryByName(string CategoryName)
        {
            Result<CategoryWithProductDTO> result = categoryServices.GetCategoryByName(CategoryName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
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
