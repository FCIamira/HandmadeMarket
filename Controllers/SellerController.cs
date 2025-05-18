using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerRepo sellerRepo;

        public SellerController(ISellerRepo sellerRepo)
        {
            this.sellerRepo = sellerRepo;
        }
        [HttpGet]
        public IActionResult GetAllSellersWithProducts()
        {
            var sellers = sellerRepo.GetAllSellersWithProducts();

            if (sellers == null || !sellers.Any())
            {
                return NotFound("No sellers found");
            }

            var sellerDtos = sellers.Select(s => new SellerWithProductsDTO
            {
                sellerId = s.UserId,
                storeName = s.storeName,
                email = s.email,
                phoneNumber = s.phoneNumber,
                createdAt = s.createdAt,
                Products = s.Products?.Select(p => new ProductDTO
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                }).ToList() ?? new List<ProductDTO>()
            }).ToList();

            return Ok(sellerDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetSellerById(string id)
        {
            Seller seller = sellerRepo.GetSellerById(id);
            if (seller == null)
            {
                return NotFound("Seller not found");
            }
            else
            {
                SellerWithProductsDTO sellerWithProductsDTO = new SellerWithProductsDTO
                {
                    sellerId = seller.UserId,
                    storeName = seller.storeName,
                    email = seller.email,
                    phoneNumber = seller.phoneNumber,
                    createdAt = seller.createdAt,
                    Products = seller.Products?.Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    }).ToList() ?? new List<ProductDTO>()
                };
                return Ok(sellerWithProductsDTO);
            }
        } 
        [HttpGet("{storeName:alpha}")]
        public IActionResult GetSellerByStoreName(string storeName)
        {
            Seller seller = sellerRepo.GetSellerWithProductsByStoreName(storeName);
            if (seller == null)
            {
                return NotFound("Seller not found");
            }
            else
            {
                SellerWithProductsDTO sellerDTO = new SellerWithProductsDTO
                {
                    sellerId = seller.UserId,
                    storeName = seller.storeName,
                    email = seller.email,
                    phoneNumber = seller.phoneNumber,
                    Products = seller.Products?.Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    }).ToList() ?? new List<ProductDTO>()
                };
                return Ok(sellerDTO);
            }
        }
        [HttpGet("product/{id:int}")]
        public IActionResult GetSellerByProductId(int id)
        {
            Seller seller = sellerRepo.GetSellerByProductId(id);

            if (seller == null)
            {
                return NotFound("Seller not found");
            }
            else
            {
                SellerDTO sellerDTO = new SellerDTO
                {
                    sellerId = seller.UserId,
                    storeName = seller.storeName,
                    email = seller.email,
                    phoneNumber = seller.phoneNumber
                };
                return Ok(sellerDTO);

            }
         }
        [HttpPost]
        public IActionResult AddSeller(SellerDTO sellerDTO)
        {
            Seller seller = new Seller
            {
                UserId = sellerDTO.sellerId,
                storeName = sellerDTO.storeName,
                email = sellerDTO.email,
                phoneNumber = sellerDTO.phoneNumber,
                createdAt = DateTime.Now
            };
            if (ModelState.IsValid)
            {
                sellerRepo.Add(seller);
                sellerRepo.Save();
                return CreatedAtAction("GetSellerById", new { id = sellerDTO.sellerId }, sellerDTO);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        public IActionResult EditSeller(int id, SellerDTO sellerDTO)
        {
            if (ModelState.IsValid)
            {
                Seller seller = sellerRepo.GetById(id);
                if (seller == null)
                {
                    return NotFound("Seller not found");
                }

                seller.storeName = sellerDTO.storeName;
                seller.email = sellerDTO.email;
                seller.phoneNumber = sellerDTO.phoneNumber;

                sellerRepo.Update(id, seller);
                sellerRepo.Save();
                return Ok(seller);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteSellerWithProductsById(string id)
        {
            Seller seller = sellerRepo.GetSellerById(id);
            if (ModelState.IsValid)
            {
                if (seller == null)
                {
                    return NotFound("Seller not found");
                }

                sellerRepo.DeleteSellerWithProductsById(id);
                sellerRepo.Save();
                return Ok(seller);
            }
            return BadRequest(ModelState);
        }

    }
}
