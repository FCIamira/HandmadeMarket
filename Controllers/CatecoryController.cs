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
            List<CategoryWithProductDTO> categories = categoryRepo.CategoryDTO();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
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
            CategoryWithProductDTO category = categoryRepo.GetCategoryDTOById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpGet("{CategoryName:alpha}")]
        public IActionResult GetCategoryByName(string CategoryName)
        {
            List<CategoryWithProductDTO> category = categoryRepo.GetCategoryByName(CategoryName);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                categoryRepo.AddCategory(categoryDto);
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
