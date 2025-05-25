using HandmadeMarket.Helpers;
using HandmadeMarket.Models;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices productServices;

        public ProductController(ProductServices productServices)
        {
            this.productServices = productServices;
        }

        #region GetAll
        [HttpGet]
        public async Task<IActionResult> GetAllProduct(int pageNumber = 1, int pageSize = 10)
        {
            var result = await productServices.GetAllProduct(pageNumber, pageSize);
            return result.ToActionResult();
        }
        #endregion

        #region GetAll Products that have sale
        [HttpGet("sale")]
        public IActionResult GetAllProductsHaveSale()
        {
            var result = productServices.GetAllProductsHaveSale();
            return result.ToActionResult();
        }
        #endregion

        #region GetProductById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await productServices.GetProductById(id);
            return result.ToActionResult();
        }
        #endregion

        #region GetProductByName
        [HttpGet("name/{name:alpha}")]
        public IActionResult GetProductByName(string name)
        {
            var result = productServices.GetProductByName(name);
            return result.ToActionResult();
        }
        #endregion

        #region CreateProduct 
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] AddProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await productServices.CreateProduct(productDTO);
            return result.ToActionResult();
        }
        #endregion

        #region Edit product
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromForm] AddProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await productServices.EditProduct(id, productDto); 
            return result.ToActionResult(); 
        }

        #endregion

        #region Delete product
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await  productServices.DeleteProductAsync(id);
            return result.ToActionResult();
        }
        #endregion

        #region Get top products
        [HttpGet("top-ordered-with-details")]
        public async Task<IActionResult> GetTopOrderedProductsWithDetails()
        {
            var result = await productServices.GetTopOrderedProductsWithDetails();
            return result.ToActionResult();
        }
        #endregion

        #region Filter products by price range
        [HttpGet("p")]
        public IActionResult FilterProductsByPrice(decimal min, decimal max)
        {
            var result = productServices.FilterProductsByPrice(min, max);
            return result.ToActionResult();
        }

        #endregion
    }
}
