using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemServices orderItemServices;
        public OrderItemController(OrderItemServices orderItemServices)
        {
            this.orderItemServices= orderItemServices;
        }
        #region Get OrderItems By OrderId
        [HttpGet]
        public IActionResult GetItemsRelatedToSpecificOrder(int orderId)
        {
            Result<IEnumerable<ViewOrderItemDto>> result = orderItemServices.GetItemsRelatedToSpecificOrder(orderId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region Get By Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Result<ViewOrderItemDto> result = orderItemServices.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        #endregion

        #region Create OrderItem
        [HttpPost]
        public IActionResult CreateOrderItem([FromBody] AddOrderItemDTO orderItem)
        {
            Result<string> result = orderItemServices.CreateOrderItem(orderItem);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region Update OrderItem
        [HttpPut("{id}")]
        public IActionResult UpdateOrderItem(int id, [FromBody] AddOrderItemDTO orderItem)
        {
            Result<string> result = orderItemServices.UpdateOrderItem(id,orderItem);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region Delete OrderItem
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            Result<string> result = orderItemServices.DeleteOrderItem(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion



        #region Get All

        [HttpGet("seller-orders")]
       // [Authorize(Roles = "Seller")]
        public IActionResult GetAllBySellerId(int pageNumber = 1, int pageSize = 5)
        {
            Result<List<OrderItemsWithOrderDetails>> result = orderItemServices.GetAllBySellerId(pageNumber,pageSize);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        #endregion

    }
}
