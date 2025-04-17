using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo orderRepo;
        private readonly IProductRepo productRepo;

        public OrderController(IOrderRepo orderRepo,IProductRepo productRepo)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
        }

        #region Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Order> orders = orderRepo.GetAll();
            List<FlatOrder_OrderItems> orderDTO = orders.SelectMany
                (order => order.Order_Items.Select(oi => new FlatOrder_OrderItems
                {

                    Order_Date = order.Order_Date,
                    Total_Price = orderRepo.CalcTotalPrice(oi.Price, oi.Quantity),
                    CustomerName = order.Customer.FirstName,
                    PricePerItem = oi.Price,
                    Quantity = oi.Quantity,
                    ProductName = oi.Product.Name,
                })).ToList();

            return Ok(orderDTO);
        }
        #endregion

        #region Get By Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Order order = orderRepo.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {

                FlatOrder_OrderItems FlatOrder = order.Order_Items.Select(oi => new FlatOrder_OrderItems
                {
                    Order_Date = order.Order_Date,
                    Total_Price = order.Total_Price,
                    CustomerName = order.Customer.FirstName,
                    PricePerItem = oi.Price,
                    Quantity = oi.Quantity,
                    ProductName = oi.Product.Name,
                }).FirstOrDefault();


                return Ok(FlatOrder);
            }


        } 
        #endregion

        #region Create Order
        [HttpPost]

        public IActionResult CreateOrder(AddOrderDTO orderDto)
        {
            if (orderDto == null || orderDto.Items == null || !orderDto.Items.Any())
            {
                return BadRequest();
            }

            // Get all product IDs from the order
            var productIds = orderDto.Items.Select(i => i.ProductId).ToList();

            // Get all products from the database in one go
            var products = productRepo.GetAll()
                            .Where(p => productIds.Contains(p.ProductId))
                            .ToDictionary(p => p.ProductId, p => p.Price);

            // If any product is not found
            if (products.Count != productIds.Count)
            {
                return NotFound("One or more products not found.");
            }

            // Build the order
            var orderItems = orderDto.Items.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = products[oi.ProductId] // Real price from DB
            }).ToList();

            var order = new Order
            {
                Order_Date = DateTime.Now,
                CustomerId = orderDto.CustomerID,
                ShipmentId = orderDto.ShipmentId,
                Order_Items = orderItems,
                Total_Price = orderItems.Sum(oi => oi.Price * oi.Quantity)
            };

            orderRepo.Add(order);
            orderRepo.Save();

            return Ok("Created");
        }
        #endregion

        #region Update Order
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, AddOrderDTO orderDto)
        {
            if (orderDto == null || orderDto.Items == null || !orderDto.Items.Any())
            {
                return BadRequest();
            }

            // Get the existing order from the DB
            var existingOrder = orderRepo.GetById(id);
            if (existingOrder == null)
            {
                return NotFound("Order not found.");
            }

            // Get all product IDs from the order
            var productIds = orderDto.Items.Select(i => i.ProductId).ToList();

            // Get actual product prices from the database
            var products = productRepo.GetAll()
                            .Where(p => productIds.Contains(p.ProductId))
                            .ToDictionary(p => p.ProductId, p => p.Price);

            if (products.Count != productIds.Count)
            {
                return NotFound("One or more products not found.");
            }

            // Update order fields
            existingOrder.CustomerId = orderDto.CustomerID;
            existingOrder.ShipmentId = orderDto.ShipmentId;
            existingOrder.Order_Date = DateTime.Now;

            // Recreate order items
            existingOrder.Order_Items = orderDto.Items.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = products[oi.ProductId]
            }).ToList();

            // Recalculate total price
            existingOrder.Total_Price = existingOrder.Order_Items.Sum(oi => oi.Price * oi.Quantity);

            // Save changes
            orderRepo.Update(id, existingOrder);
            orderRepo.Save();

            return Ok("Updated");
        }


        #endregion

        #region Delete Order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = orderRepo.GetById(id);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            orderRepo.Remove(id);
            orderRepo.Save();
            return Ok("Deleted");
        }
        #endregion

    }
}
