using HandmadeMarket.DTO.OrderItemDTOs;
using HandmadeMarket.Enum;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace HandmadeMarket.Services
{
    public class OrderItemServices
    {
        private readonly IOrderItemRepo orderItemRepo;
        private readonly IProductRepo productRepo;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderItemServices(IHttpContextAccessor httpContextAccessor, IOrderItemRepo orderItemRepo, IProductRepo productRepo)
        {
            this.orderItemRepo = orderItemRepo;
            this.productRepo = productRepo;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Result<IEnumerable<ViewOrderItemDto>> GetItemsRelatedToSpecificOrder(int orderId)
        {
            var orderItems = orderItemRepo.GetOrderItemsByOrderId(orderId);
            var orderItemDTOs = orderItems.Select(o => new ViewOrderItemDto
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
            var orderItem = orderItemRepo.GetById(id);
            if (orderItem == null)
            {
                return Result<ViewOrderItemDto>.Failure(ErrorCode.NotFound, "Order item not found");
            }

            var dto = new ViewOrderItemDto
            {
                OrderItemId = orderItem.OrderItemId,
                OrderId = orderItem.OrderId,
                Quantity = orderItem.Quantity,
                PricePerItem = orderItem.Price,
                ProductName = orderItem.Product.Name
            };
            return Result<ViewOrderItemDto>.Success(dto);
        }

        public Result<string> CreateOrderItem(AddOrderItemDTO orderItem)
        {
            if (orderItem == null)
            {
                return Result<string>.Failure(ErrorCode.BadRequest, "Order item is null");
            }
            if (orderItem.Quantity <= 0)
            {
                return Result<string>.Failure(ErrorCode.BadRequest, "Quantity must be greater than 0");
            }

            var product = productRepo.GetById(orderItem.ProductId);
            if (product == null)
            {
                return Result<string>.Failure(ErrorCode.NotFound, "Product not found");
            }

            var newOrderItem = new OrderItem
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

        #region UpdateOrderItem
        public Result<string> UpdateOrderItem(int id, AddOrderItemDTO orderItem)
        {
            if (orderItem == null)
            {
                return Result<string>.Failure(ErrorCode.BadRequest, "Order item is null");
            }
            if (orderItem.Quantity <= 0)
            {
                return Result<string>.Failure(ErrorCode.BadRequest, "Quantity must be greater than 0");
            }

            var existingOrderItem = orderItemRepo.GetById(id);
            if (existingOrderItem == null)
            {
                return Result<string>.Failure(ErrorCode.NotFound, "Order item not found");
            }

            var product = productRepo.GetById(orderItem.ProductId);
            if (product == null)
            {
                return Result<string>.Failure(ErrorCode.NotFound, "Product not found");
            }

            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.Price = product.Price;
            existingOrderItem.ProductId = orderItem.ProductId;

            orderItemRepo.Update(id, existingOrderItem);
            orderItemRepo.Save();

            return Result<string>.Success("Updated");
        }

        #endregion

        #region DeleteOrderItem
        public Result<string> DeleteOrderItem(int id)
        {
            var orderItem = orderItemRepo.GetById(id);
            if (orderItem == null)
            {
                return Result<string>.Failure(ErrorCode.NotFound, "Order item not found");
            }

            orderItemRepo.Remove(id);
            orderItemRepo.Save();

            return Result<string>.Success("Deleted");
        }

        #endregion


        #region GetAllBySellerId
        public Result<List<OrderItemsWithOrderDetails>> GetAllBySellerId(int pageNumber = 1, int pageSize = 5)
        {
            var sellerId = httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;

            if (string.IsNullOrEmpty(sellerId))
            {
                return Result<List<OrderItemsWithOrderDetails>>.Failure(ErrorCode.Unauthorized, "Seller not authenticated");
            }

            var orders = orderItemRepo.GetAllBySellerId(sellerId, pageSize, pageNumber);

            var dto = orders
                .Where(oi => oi.Product != null && oi.Order?.Customer != null && oi.Order?.Shipment != null)
                .Select(orderItem => new OrderItemsWithOrderDetails
                {
                    ProductName = orderItem.Product.Name,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity,
                    CustomerAddress = orderItem.Order.Customer.Address,
                    CustomerName = orderItem.Order.Customer.FirstName,
                    ShipmentDate = orderItem.Order.Shipment.ShipmentDate
                }).ToList();

            return Result<List<OrderItemsWithOrderDetails>>.Success(dto);
        }

        #endregion
    }
}
