using HandmadeMarket.DTO;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryRepo categoryRepo;
        CategoryServices categoryServices;
        public CategoryController(ICategoryRepo categoryRepo, CategoryServices categoryServices)
        {
            this.categoryRepo = categoryRepo;
            this.categoryServices = categoryServices;
        }
        #region GetAll
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

        #endregion

        #region GetByID
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
        #endregion


        #region GetCategoryByName
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

        #endregion


        #region AddCategory
        [HttpPost]
        public IActionResult AddCategory(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                var result = categoryServices.AddCategory(categoryDto);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);


        }

        #endregion

        #region UpdateCategory
        [HttpPut]
        public IActionResult UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var result = categoryServices.UpdateCategory(id, categoryDTO);

                if (result.IsSuccess)
                {

                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region DeleteCategory
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            if (ModelState.IsValid)
            {
                var result = categoryServices.DeleteCategory(id);

                if (result.IsSuccess)
                {

                    return Ok(result);
                }
            }
            return BadRequest(ModelState);
        }

        #endregion


    }
}
