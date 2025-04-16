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
            IQueryable<SellerWithProductsDTO> sellers= sellerRepo.GetAllSellersWithProducts();
            if(ModelState.IsValid)
            {
                if(sellers !=null )
                    return Ok(sellers);
                else
                    return NotFound("No sellers found");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetSellerById(int id)
        {
            SellerWithProductsDTO seller = sellerRepo.GetSellerWithProductsById(id);
            if(ModelState.IsValid)
            {
                if(seller != null)
                    return Ok(seller);
                else
                    return NotFound("Seller not found");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("{storeName:alpha}")]
        public IActionResult GetSellerByStoreName(string storeName)
        {
            SellerWithProductsDTO seller = sellerRepo.GetSellerWithProductsByStoreName(storeName);
            if (ModelState.IsValid)
            {
                if (seller != null)
                    return Ok(seller);
                else
                    return NotFound("Seller not found");
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public IActionResult AddSeller(SellerDTO sellerDTO)
        {
            if (ModelState.IsValid)
            {
                sellerRepo.AddSeller(sellerDTO);
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
                sellerRepo.EditSeller(id, sellerDTO);
                sellerRepo.Save();
                return Ok(seller);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteSellerWithProductsById(int id)
        {
            Seller seller = sellerRepo.GetById(id);
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
