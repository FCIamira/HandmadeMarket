using HandmadeMarket.Helpers; // ← مهم لإضافة ToActionResult()
using HandmadeMarket.Services;
using HandmadeMarket.DTO.OrderDTOs;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderServices orderServices;

        public OrderController(OrderServices orderServices)
        {
            this.orderServices = orderServices;
        }

        #region Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = orderServices.GetAll();
            return result.ToActionResult();
        }
        #endregion

        #region Get By Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = orderServices.GetById(id);
            return result.ToActionResult();
        }
        #endregion

        #region Create Order
        [HttpPost]
        public IActionResult CreateOrder([FromBody] AddOrderDTO orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = orderServices.CreateOrder(orderDto);
            return result.ToActionResult();
        }
        #endregion

        #region Update Order
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] AddOrderDTO orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = orderServices.UpdateOrder(id, orderDto);
            return result.ToActionResult();
        }
        #endregion

        #region Delete Order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var result = orderServices.DeleteOrder(id);
            return result.ToActionResult();
        }
        #endregion
    }
}
