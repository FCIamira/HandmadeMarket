using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Services
{
    public class OrderItemServices
    {
        private readonly IOrderItemRepo orderItemRepo;
        private readonly IProductRepo productRepo;
        private readonly IHttpContextAccessor httpContextAccessor;
        public OrderItemServices(IHttpContextAccessor httpContextAccessor,IOrderItemRepo orderItemRepo, IProductRepo productRepo)
        {
            this.orderItemRepo = orderItemRepo;
            this.productRepo = productRepo;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Result<IEnumerable<ViewOrderItemDto>> GetItemsRelatedToSpecificOrder(int orderId)
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
            return Result<IEnumerable<ViewOrderItemDto>>.Success(orderItemDTOs);
        }

        public Result<ViewOrderItemDto> GetById(int id)
        {
            OrderItem orderItem = orderItemRepo.GetById(id);

            if (orderItem == null)
            {
                return Result<ViewOrderItemDto>.Failure("Not Found");
            }
            ViewOrderItemDto orderItemDTO = new ViewOrderItemDto
            {
                OrderItemId = orderItem.OrderItemId,
                OrderId = orderItem.OrderId,
                Quantity = orderItem.Quantity,
                PricePerItem = orderItem.Price,
                ProductName = orderItem.Product.Name
            };
            return Result<ViewOrderItemDto>.Success(orderItemDTO);

        }

        public Result<string> CreateOrderItem(AddOrderItemDTO orderItem)
        {
            if (orderItem == null)
            {
                return Result<string>.Failure("Order item is null");
            }
            if (orderItem.Quantity <= 0)
            {
                return Result<string>.Failure("Quantity must be greater than 0");
            }
            Product product = productRepo.GetById(orderItem.ProductId);
            if (product == null)
            {
                return Result<string>.Failure("Product not found");
            }

                OrderItem newOrderItem = new OrderItem
                {
                    OrderId = orderItem.OrderId,
                    Quantity = orderItem.Quantity,
                    Price = product.Price,
                    ProductId = orderItem.ProductId
                };
                orderItemRepo.Add(newOrderItem);
                orderItemRepo.Save();
                return Result<string>.Success("Created");
            

        }

        public Result<string> UpdateOrderItem(int id,AddOrderItemDTO orderItem)
        {
            if (orderItem == null)
            {
                return Result<string>.Failure("Order item is null");
            }
            if (orderItem.Quantity <= 0)
            {
                return Result<string>.Failure("Quantity must be greater than 0");
            }
            OrderItem existingOrderItem = orderItemRepo.GetById(id);
            Product product = productRepo.GetById(orderItem.ProductId);
            if (existingOrderItem == null)
            {
                return Result<string>.Failure("Order item not found");
            }
            else
            {
                existingOrderItem.Quantity = orderItem.Quantity;
                existingOrderItem.Price = product.Price;
                orderItemRepo.Update(id, existingOrderItem);
                orderItemRepo.Save();
                return Result<string>.Success("Updated");
            }
        }
        public Result<string> DeleteOrderItem(int id)
        {
            OrderItem orderItem = orderItemRepo.GetById(id);
            if (orderItem == null)
            {
                return Result<string>.Failure("Order item not found");
            }
            orderItemRepo.Remove(id);
            orderItemRepo.Save();
            return Result<string>.Success("Deleted");
        }

        public Result<List<OrderItemsWithOrderDetails>> GetAllBySellerId(int pageNumber = 1, int pageSize = 5)
        {
            var sellerId = httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
            if (sellerId != null)
            {
                IEnumerable<OrderItem> orders = orderItemRepo.GetAllBySellerId(sellerId, pageSize, pageNumber);
                List<OrderItemsWithOrderDetails> DTO = new List<OrderItemsWithOrderDetails>();

                foreach (OrderItem orderItem in orders)
                {
                    if (orderItem.Product != null && orderItem.Order?.Customer != null && orderItem.Order?.Shipment != null)
                    {
                        DTO.Add(new OrderItemsWithOrderDetails
                        {
                            ProductName = orderItem.Product.Name,
                            Price = orderItem.Price,
                            Quantity = orderItem.Quantity,
                            CustomerAddress = orderItem.Order.Customer.Address,
                            CustomerName = orderItem.Order.Customer.FirstName,
                            ShipmentDate = orderItem.Order.Shipment.ShipmentDate
                        });
                    }


                }
                return Result<List<OrderItemsWithOrderDetails>>.Success(DTO);

            }
                return Result<List<OrderItemsWithOrderDetails>>.Failure("sell doesn't exist");
        }
    }
}
