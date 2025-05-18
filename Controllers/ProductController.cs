using HandmadeMarket.DTO;
<<<<<<< HEAD
using HandmadeMarket.Models;
=======
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
using Microsoft.AspNetCore.Authorization;
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
<<<<<<< HEAD
        //[Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult GetAllProduct(int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<Product> products = productRepo.GetAll();

            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }

            // الحسابات الأساسية للصفحات
            int totalCount = products.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // تطبيق التجزئة
            var pagedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // التحويل إلى DTO
            List<ProductDTO> productDTO = pagedProducts.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Image = string.IsNullOrEmpty(p.Image) ? null : $"{Request.Scheme}://{Request.Host}{p.Image}",
                PriceAfterSale = p.PriceAfterSale > 0 ? p.PriceAfterSale : p.Price,
                SalePercentage = p.SalePercentage > 0 ? p.SalePercentage : 0,
            }).ToList();

            // تغليف النتائج مع معلومات الصفحات
            var response = new
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Data = productDTO
            };

            return Ok(response);
        }

        //public IActionResult GetAllProduct()
        //{
        //    IEnumerable<Product> products = productRepo.GetAll();
        //    List<ProductDTO> productDTO = products.Select(products => new ProductDTO
        //    {
        //        ProductId = products.ProductId,
        //        Name = products.Name,
        //        Description = products.Description,
        //        Price = products.Price,
        //        Stock = products.Stock,
        //        Image = string.IsNullOrEmpty(products.Image) ? null : $"{Request.Scheme}://{Request.Host}{products.Image}",
        //        PriceAfterSale = products.PriceAfterSale>0 ? products.PriceAfterSale:products.Price,
        //        SalePercentage = products.SalePercentage>0 ? products.SalePercentage:0,


        //    }).ToList();


        //    if (products == null)
        //    {
        //        return NotFound("Product not found");
        //    }
        //    return Ok(productDTO);
        //}
=======
        [HttpGet]
        [Authorize(Roles ="Admin")]

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
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
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
<<<<<<< HEAD
                    //Image = product.Image,
                    Image = string.IsNullOrEmpty(product.Image) ? null : $"{Request.Scheme}://{Request.Host}{product.Image}",

=======
                    Image = product.Image,
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
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
<<<<<<< HEAD
                    //Image = product.Image,
                    Image = string.IsNullOrEmpty(product.Image) ? null : $"{Request.Scheme}://{Request.Host}{product.Image}",

=======
                    Image = product.Image,
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
                    PriceAfterSale = product.PriceAfterSale > 0 ? product.PriceAfterSale : product.Price,
                    SalePercentage = product.SalePercentage > 0 ? product.SalePercentage : 0,
                };
                return Ok(productDTO);
            }
            
        }
        #endregion

<<<<<<< HEAD
        //#region CreateProduct
        //[HttpPost]
        //public IActionResult CreateProduct(AddProductDTO productDTO)
        //{
        //    Product product = new Product()
        //    {

        //        Description = productDTO.Description,
        //        Name = productDTO.Name,
        //        Price = productDTO.Price,
        //        Stock = productDTO.Stock,
        //        Image = productDTO.Image,
        //        categoryId = productDTO.categoryId,
        //        sellerId = productDTO.sellerId,
        //        HasSale = productDTO.HasSale,
        //        SalePercentage = productDTO.SalePercentage,
        //        PriceAfterSale = productRepo.CalcPriceAfterSale(productDTO.Price, productDTO.SalePercentage)
        //    };
        //    if (ModelState.IsValid)
        //    {
        //        productRepo.Add(product);
        //        productRepo.Save();
        //        Product product1 = productRepo.GetById(product.ProductId);
        //        return CreatedAtAction("GetProductById", new { id = product1.ProductId }, productDTO);
        //    }
        //    else
        //        return BadRequest(ModelState);

        //}
        //#endregion


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] AddProductDTO productDTO)
        {
            string imagePath = null;
            if (productDTO.Image != null && productDTO.Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productDTO.Image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDTO.Image.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + uniqueFileName; // هذا اللي نحفظه في قاعدة البيانات
            }

            Product product = new Product()
            {
=======
        #region CreateProduct
        [HttpPost]
        public IActionResult CreateProduct(AddProductDTO productDTO)
        {
            Product product = new Product()
            {

>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
                Description = productDTO.Description,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
<<<<<<< HEAD
                Image = imagePath,
=======
                Image = productDTO.Image,
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
                categoryId = productDTO.categoryId,
                sellerId = productDTO.sellerId,
                HasSale = productDTO.HasSale,
                SalePercentage = productDTO.SalePercentage,
                PriceAfterSale = productRepo.CalcPriceAfterSale(productDTO.Price, productDTO.SalePercentage)
            };
<<<<<<< HEAD

=======
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
            if (ModelState.IsValid)
            {
                productRepo.Add(product);
                productRepo.Save();
                Product product1 = productRepo.GetById(product.ProductId);
<<<<<<< HEAD

                // ارجاع المسار في DTO
                var resultDTO = new ProductDTO
                {
                    ProductId = product1.ProductId,
                    Name = product1.Name,
                    Description = product1.Description,
                    Price = product1.Price,
                    Stock = product1.Stock,
                    //Image = product1.Image,
                    Image = string.IsNullOrEmpty(product.Image) ? null : $"{Request.Scheme}://{Request.Host}{product.Image}",

                    PriceAfterSale = product1.PriceAfterSale,
                    SalePercentage = product1.SalePercentage ?? 0
                };

                return CreatedAtAction("GetProductById", new { id = product1.ProductId }, resultDTO);
            }
            else
                return BadRequest(ModelState);
        }

        #region Edit product
        //[HttpPut("{id}")]
        //public IActionResult EditProduct(int id, Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Product existingProduct = productRepo.GetById(id);
        //    if (existingProduct == null)
        //    {
        //        return NotFound("Product not found");
        //    }

        //    existingProduct.Description = product.Description;
        //    existingProduct.Name = product.Name;
        //    existingProduct.Price = product.Price;
        //    existingProduct.Stock = product.Stock;
        //    existingProduct.Image = product.Image;
        //    existingProduct.categoryId = product.categoryId;
        //    existingProduct.sellerId = product.sellerId;

        //    productRepo.Update(id, existingProduct);
        //    productRepo.Save();

        //    return Ok(existingProduct);
        //}
        #region Edit product
=======
                return CreatedAtAction("GetProductById", new { id = product1.ProductId }, productDTO);
            }
            else
                return BadRequest(ModelState);

        }
        #endregion

        #region Edit product
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
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

<<<<<<< HEAD
            // رجعي DTO
            var productDTO = new ProductDTO
            {
                ProductId = existingProduct.ProductId,
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
                Stock = existingProduct.Stock,
                Image = string.IsNullOrEmpty(existingProduct.Image) ? null : $"{Request.Scheme}://{Request.Host}{existingProduct.Image}",
                SalePercentage = existingProduct.SalePercentage ?? 0,
                PriceAfterSale = existingProduct.PriceAfterSale > 0 ? existingProduct.PriceAfterSale : existingProduct.Price
            };

            return Ok(productDTO);
        }
        #endregion
=======
            return Ok(existingProduct);
        }
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007


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