using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepo orderItemRepo;
        private readonly IProductRepo productRepo;
        public OrderItemController(IOrderItemRepo orderItemRepo, IProductRepo productRepo)
        {
            this.orderItemRepo = orderItemRepo;
            this.productRepo = productRepo;
        }
        #region Get OrderItems By OrderId
        [HttpGet]
        public IActionResult GetItemsRelatedToSpecificOrder(int orderId)
        {
            IEnumerable<OrderItem> orderItems = orderItemRepo.GetOrderItemsByOrderId(orderId);
            IEnumerable<ViewOrderItemDto> orderItemDTOs = orderItems.Select(o => new ViewOrderItemDto
            {
                OrderItemId = o.OrderItemId,
                OrderId = o.OrderId,
                Quantity = o.Quantity,
                PricePerItem = o.Price,
                ProductName = o.Product.Name
            });
            return Ok(orderItemDTOs);
        }
        #endregion

        #region Get By Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            OrderItem orderItem = orderItemRepo.GetById(id);
            
            if (orderItem == null)
            {
                return NotFound();
            }
            else
            {
                ViewOrderItemDto orderItemDTO = new ViewOrderItemDto
                {
                    OrderItemId = orderItem.OrderItemId,
                    OrderId = orderItem.OrderId,
                    Quantity = orderItem.Quantity,
                    PricePerItem = orderItem.Price,
                    ProductName = orderItem.Product.Name
                };
                return Ok(orderItemDTO);
            }
        }
        #endregion

        #region Create OrderItem
        [HttpPost]
        public IActionResult CreateOrderItem([FromBody] AddOrderItemDTO orderItem)
        {
            if (orderItem == null)
            {
                return BadRequest("Order item is null");
            }
            if (orderItem.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than 0");
            }
            Product product = productRepo.GetById(orderItem.ProductId);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            else {

                OrderItem newOrderItem = new OrderItem
                {
                    OrderId = orderItem.OrderId,
                    Quantity = orderItem.Quantity,
                    Price = product.Price,
                    ProductId = orderItem.ProductId
                };
                orderItemRepo.Add(newOrderItem);
                orderItemRepo.Save();
                return Ok("Created");
            }
          
        }
        #endregion

        #region Update OrderItem
        [HttpPut("{id}")]
        public IActionResult UpdateOrderItem(int id, [FromBody] AddOrderItemDTO orderItem)
        {
            if (orderItem == null)
            {
                return BadRequest("Order item is null");
            }
            if (orderItem.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than 0");
            }
            OrderItem existingOrderItem = orderItemRepo.GetById(id);
            Product product = productRepo.GetById(orderItem.ProductId);
            if (existingOrderItem == null)
            {
                return NotFound("Order item not found");
            }
            else
            {
                existingOrderItem.Quantity = orderItem.Quantity;
                existingOrderItem.Price = product.Price;
                orderItemRepo.Update(id,existingOrderItem);
                orderItemRepo.Save();
                return Ok("Updated");
            }
        }
        #endregion

        #region Delete OrderItem
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            OrderItem orderItem = orderItemRepo.GetById(id);
            if (orderItem == null)
            {
                return NotFound("Order item not found");
            }
            else
            {
                orderItemRepo.Remove(id);
                orderItemRepo.Save();
                return Ok("Deleted");
            }
        }
        #endregion

    }
}
