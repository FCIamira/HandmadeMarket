using HandmadeMarket.Services;
using HandmadeMarket.Helpers; 
using Microsoft.AspNetCore.Mvc;
using HandmadeMarket.DTO.CategoryDTOs;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServices categoryServices;

        public CategoryController(ICategoryRepo categoryRepo, CategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = categoryServices.GetAllCategoriesWithProducts();
            return result.ToActionResult();
        }
        #endregion

        #region GetByID
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = categoryServices.GetById(id);
            return result.ToActionResult();
        }
        #endregion

        #region GetCategoryByName
        [HttpGet("by-name/{CategoryName:alpha}")]
        public IActionResult GetCategoryByName(string CategoryName)
        {
            var result = categoryServices.GetCategoryByName(CategoryName);
            return result.ToActionResult();
        }
        #endregion

        #region AddCategory
        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = categoryServices.AddCategory(categoryDto);
            return result.ToActionResult();
        }
        #endregion

        #region UpdateCategory
        [HttpPut("{id:int}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = categoryServices.UpdateCategory(id, categoryDTO);
            return result.ToActionResult();
        }
        #endregion

        #region DeleteCategory
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = categoryServices.DeleteCategory(id);
            return result.ToActionResult();
        }
        #endregion

    }
}
