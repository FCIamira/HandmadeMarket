using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
      private   ICartRepo cartRepo { get; set; }
        public CartController(ICartRepo cartRepo) { 
            this.cartRepo = cartRepo;
        }
        [HttpGet]
        public IActionResult GetAll () { 
            List<CartWithProductDTO> carts = cartRepo.CategoryDTO().ToList();
            if (carts != null) {

                return Ok(carts);
            }
            return NotFound();
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
           Cart cart = cartRepo.GetById(id);
            if (cart != null)
            {

                return Ok(cart);
            }
            return NotFound("Invalid Id");
        }
        [HttpPost]
        public IActionResult Add([FromBody] CartWithProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = new Cart
            {
                Id = dto.Id,
                Quantity = dto.Quantity,
                ProductId = dto.ProductId,
                CustomerId = dto.CustomerId ?? "default-customer-id" // Set proper value
            };

            cartRepo.Add(cart);
            cartRepo.Save();

            return CreatedAtAction(nameof(Add), new { id = cart.Id }, dto);
        }


        //////////////////Delete
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Cart cart = cartRepo.GetById(id);
            if (cart != null)
            {
                cartRepo.Remove(id);
                cartRepo.Save();
                return Content("Cart is deleted sucessfully");
            }
            return NotFound("Invalid Id");
        }

        ////////////////////////////////update
        
        [HttpPut("{id:int}")]
        public IActionResult Update(int id,UpdateCartDTO cartFromReq) {
            Cart cart1 = cartRepo.GetById(id);
            if (cart1 != null)
            {
                cart1.Quantity = cartFromReq.Quantity;
                cartRepo.Update(id,cart1);
                cartRepo.Save();
                return Content("Cart is updated sucessfully");
            }
            return NotFound("Invalid Id");

        }
        
    }
}
