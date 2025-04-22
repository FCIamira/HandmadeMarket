using HandmadeMarket.DTO;
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
            Product product = productRepo.GetById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            else
            {
                ProductDTO productDTO = new ProductDTO
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    Image = product.Image,
                    PriceAfterSale = product.PriceAfterSale > 0 ? product.PriceAfterSale : product.Price,
                    SalePercentage = product.SalePercentage > 0 ? product.SalePercentage : 0,
                };

                return Ok(productDTO);
            }
          
        }


        #endregion

        #region GetProductByName
        [HttpGet("name/{name:alpha}")]
        public IActionResult GetProductByName(string name)
        {
            Product product = productRepo.GetProductByName(name);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            else
            {
                ProductDTO productDTO = new ProductDTO
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    Image = product.Image,
                    PriceAfterSale = product.PriceAfterSale > 0 ? product.PriceAfterSale : product.Price,
                    SalePercentage = product.SalePercentage > 0 ? product.SalePercentage : 0,
                };
                return Ok(productDTO);
            }
            
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
        public IActionResult EditProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product existingProduct = productRepo.GetById(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            existingProduct.Description = product.Description;
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.Image = product.Image;
            existingProduct.categoryId = product.categoryId;
            existingProduct.sellerId = product.sellerId;

            productRepo.Update(id, existingProduct);
            productRepo.Save();

            return Ok(existingProduct);
        }


        #endregion


        #region delete product

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            Product product = productRepo.GetById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            productRepo.Remove(id);
            productRepo.Save();
            return NoContent();
        }
        #endregion

        #region get top products
        [HttpGet("top-ordered-with-details")]
        public async Task<ActionResult> GetTopOrderedProductsWithDetails()
        {
            
            var topProducts = await productRepo.GetTopProductsByHighestNumberOfOrder();
            return Ok(topProducts);
        }
        #endregion


        [HttpGet("p")]
        public IActionResult FilterProductsByPrice(decimal min, decimal max) { 
        List<ProductDTO> products=productRepo.GetProductsByRanges(min, max);
            return Ok(products);
        
        }

    }
}