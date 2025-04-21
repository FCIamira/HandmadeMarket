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
        #region GetAll
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
                Stock = products.Stock,
                PriceAfterSale = products.PriceAfterSale>0 ? products.PriceAfterSale:products.Price,
                SalePercentage = products.SalePercentage>0 ? products.SalePercentage:0,

            }).ToList();
            

            if (products == null)
            {
                return NotFound("Product not found");
            }
            return Ok(productDTO);
        }
        #endregion

        #region GetAll Products that have sale
        [HttpGet("sale")]
        public IActionResult GetAllProductsHaveSale()
        {
            IEnumerable<Product> products = productRepo.GetProductsHaveSale();
            List<ProductDTO> productDTO = products.Select(products => new ProductDTO
            {
                ProductId = products.ProductId,
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
                Stock = products.Stock,
                PriceAfterSale = products.PriceAfterSale,
                SalePercentage = products.SalePercentage,
            }).ToList();

            if (products == null)
            {
                return NotFound("Product not found");
            }
            return Ok(productDTO);
        }

        #endregion

        #region GetProductById
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


        #endregion

        #region GetProductByName
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
        #endregion

        #region CreateProduct
        [HttpPost]
        public IActionResult CreateProduct(AddProductDTO productDTO)
        {
            Product product = new Product()
            {

                Description = productDTO.Description,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                Image = productDTO.Image,
                categoryId = productDTO.categoryId,
                sellerId = productDTO.sellerId,
                HasSale = productDTO.HasSale,
                SalePercentage = productDTO.SalePercentage,
                PriceAfterSale = productRepo.CalcPriceAfterSale(productDTO.Price, productDTO.SalePercentage)
            };
            if (ModelState.IsValid)
            {
                productRepo.Add(product);
                productRepo.Save();
                Product product1 = productRepo.GetById(product.ProductId);
                return CreatedAtAction("GetProductById", new { id = product1.ProductId }, productDTO);
            }
            else
                return BadRequest(ModelState);

        }
        #endregion

        #region Edit product
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

        #endregion

        #region Delete
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
        #endregion


        [HttpGet("p")]
        public IActionResult FilterProductsByPrice(decimal min, decimal max) { 
        List<ProductDTO> products=productRepo.GetProductsByRanges(min, max);
            return Ok(products);
        
        }

    }
}
