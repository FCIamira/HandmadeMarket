using HandmadeMarket.Helpers;
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
            var result = orderItemServices.GetItemsRelatedToSpecificOrder(orderId);
            return result.ToActionResult();
        }
        #endregion

        #region Get By Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = orderItemServices.GetById(id);
            return result.ToActionResult();
        }

        #endregion

        #region Create OrderItem
        [HttpPost]
       
            public IActionResult CreateOrderItem([FromBody] AddOrderItemDTO orderItem)
            {
                var result = orderItemServices.CreateOrderItem(orderItem);
                return result.ToActionResult();
            }
        
        #endregion

        #region Update OrderItem
        [HttpPut("{id}")]
        public IActionResult UpdateOrderItem(int id, [FromBody] AddOrderItemDTO orderItem)
        {
            var result = orderItemServices.UpdateOrderItem(id, orderItem);
            return result.ToActionResult();
        }
        #endregion

        #region Delete OrderItem
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            var result = orderItemServices.DeleteOrderItem(id);
            return result.ToActionResult();
        }
        #endregion



        #region Get All

        [HttpGet("seller-orders")]
        // [Authorize(Roles = "Seller")]
        public IActionResult GetAllBySellerId(int pageNumber = 1, int pageSize = 5)
        {
            var result = orderItemServices.GetAllBySellerId(pageNumber, pageSize);
            return result.ToActionResult();
        }
        #endregion

    }
}
