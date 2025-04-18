using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo productRepo;

        public ProductController( IProductRepo productRepo) 
        {
            this.productRepo = productRepo;
        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {
           IEnumerable<Product> products = productRepo.GetAll();
            List<ProductDTO> productDTO = products.Select(products => new ProductDTO
            {
                ProductId = products.ProductId,
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
                Stock = products.Stock

            }).ToList();

            if (products == null)
            {
                return NotFound("Product not found");
            }
            return Ok(productDTO);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            ProductDTO product = productRepo.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        [HttpGet("name/{name:alpha}")]
        public IActionResult GetProductByName(string name)
        {
            ProductDTO product = productRepo.GetProductByName(name);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult CreateProduct(AddProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                productRepo.AddProduct(productDTO);
                productRepo.Save();
                return CreatedAtAction("GetProductById", new { id = productDTO.ProductId }, productDTO);
            }
            return BadRequest(ModelState);

        }
        [HttpPut("{id}")]
        public IActionResult EditProduct(int id, AddProductDTO productDTO)
        {
            ProductDTO product = productRepo.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            else if (ModelState.IsValid)
            {
                productRepo.EditProduct(id, productDTO);
                productRepo.Save();
                return Ok(productDTO);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            ProductDTO product = productRepo.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            productRepo.DeleteProduct(id);
            productRepo.Save();
            return NoContent();
        }
    }
}
