using HandmadeMarket.DTO;
using HandmadeMarket.DTO;
using HandmadeMarket.Models;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices productRepo1;
        private readonly IProductRepo productRepo;

        public ProductController(ProductServices productRepo1, IProductRepo productRepo)
        {
            this.productRepo1 = productRepo1;
            this.productRepo = productRepo;
        }
        #region GetAll

        [HttpGet]
        public IActionResult GetAllProduct(int pageNumber = 1, int pageSize = 10)
        {

            var result= productRepo1.GetAllProduct(pageNumber, pageSize);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion


        #region GetAll Products that have sale
        [HttpGet("sale")]
        public IActionResult GetAllProductsHaveSale()
        {
            var result = productRepo1.GetAllProductsHaveSale();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        #endregion

        #region GetProductById
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var result = productRepo1.GetProductById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }


        #endregion

        #region GetProductByName
        [HttpGet("name/{name:alpha}")]
        public IActionResult GetProductByName(string name)
        {
            var result = productRepo1.GetProductByName(name);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion


        #region CreateProduct 
        [Authorize(Roles = "Seller")]

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] AddProductDTO productDTO)
        {
            var result = await productRepo1.CreateProduct(productDTO);
            if (ModelState.IsValid)
            {
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        

        }

        #endregion


        #region Edit product

        [HttpPut("{id}")]
        public IActionResult EditProduct(int id, [FromForm] AddProductDTO productDto)
        {
            var result =  productRepo1.EditProduct(id, productDto);
            if (ModelState.IsValid)
            {
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);

        }

        #endregion

        #region delete product

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var result = productRepo1.DeleteProduct(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region get top products
        [HttpGet("top-ordered-with-details")]
        public async Task<ActionResult> GetTopOrderedProductsWithDetails()
        {
            var result = await productRepo1.GetTopOrderedProductsWithDetails();
            if (result.IsSuccess)
                return Ok(result.Data); 
            return BadRequest(result.Error);
        }

        #endregion


        [HttpGet("p")]
        public IActionResult FilterProductsByPrice(decimal min, decimal max) {
            List<ProductDTO> products = productRepo.GetProductsByRanges(min, max);
            return Ok(products);

        }

    }
}