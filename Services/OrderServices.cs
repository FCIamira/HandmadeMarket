using HandmadeMarket.DTO.OrderItemDTOs;
using HandmadeMarket.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HandmadeMarket.Services
{
    public class OrderServices
    {
        private readonly IOrderRepo orderRepo;
        private readonly IProductRepo productRepo;

        public OrderServices(IProductRepo productRepo, IOrderRepo orderRepo)
        {
            this.productRepo = productRepo;
            this.orderRepo = orderRepo;
        }

        public Result<List<FlatOrder_OrderItems>> GetAll()
        {
            var orders = orderRepo.GetAll();
            if (orders == null || !orders.Any())
            {
                return Result<List<FlatOrder_OrderItems>>.Failure(ErrorCode.NotFound, "No orders found.");
            }

            var orderDTO = orders.SelectMany(order => order.Order_Items.Select(oi => new FlatOrder_OrderItems
            {
                Order_Date = order.Order_Date,
                Total_Price = orderRepo.CalcTotalPrice(oi.Price, oi.Quantity),
                CustomerName = order.Customer.FirstName,
                PricePerItem = oi.Price,
                Quantity = oi.Quantity,
                ProductName = oi.Product.Name,
            })).ToList();

            return Result<List<FlatOrder_OrderItems>>.Success(orderDTO);
        }

        public Result<FlatOrder_OrderItems> GetById(int id)
        {
            var order = orderRepo.GetById(id);
            if (order == null)
            {
                return Result<FlatOrder_OrderItems>.Failure(ErrorCode.NotFound, "Order not found.");
            }

            var flatOrder = order.Order_Items.Select(oi => new FlatOrder_OrderItems
            {
                Order_Date = order.Order_Date,
                Total_Price = order.Total_Price,
                CustomerName = order.Customer.FirstName,
                PricePerItem = oi.Price,
                Quantity = oi.Quantity,
                ProductName = oi.Product.Name,
            }).FirstOrDefault();

            return Result<FlatOrder_OrderItems>.Success(flatOrder);
        }

        public Result<string> CreateOrder(AddOrderDTO orderDto)
        {
            if (orderDto == null || orderDto.Items == null || !orderDto.Items.Any())
            {
                return Result<string>.Failure(ErrorCode.BadRequest, "Order data or items are missing.");
            }

            var productIds = orderDto.Items.Select(i => i.ProductId).ToList();

            var products = productRepo.GetAll()
                .Where(p => productIds.Contains(p.ProductId))
                .ToDictionary(p => p.ProductId, p => p.Price);

            if (products.Count != productIds.Count)
            {
                return Result<string>.Failure(ErrorCode.NotFound, "One or more products not found.");
            }

            var orderItems = orderDto.Items.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = products[oi.ProductId]
            }).ToList();

            var order = new Order
            {
                Order_Date = DateTime.UtcNow,
                CustomerId = orderDto.CustomerID,
                ShipmentId = orderDto.ShipmentId,
                Order_Items = orderItems,
                Total_Price = orderItems.Sum(oi => oi.Price * oi.Quantity)
            };

            orderRepo.Add(order);
            orderRepo.Save();

            return Result<string>.Success("Order created successfully.");
        }

        public Result<string> UpdateOrder(int id, AddOrderDTO orderDto)
        {
            if (orderDto == null || orderDto.Items == null || !orderDto.Items.Any())
            {
                return Result<string>.Failure(ErrorCode.BadRequest, "Order data or items are missing.");
            }

            var existingOrder = orderRepo.GetById(id);
            if (existingOrder == null)
            {
                return Result<string>.Failure(ErrorCode.NotFound, "Order not found.");
            }

            var productIds = orderDto.Items.Select(i => i.ProductId).ToList();

            var products = productRepo.GetAll()
                .Where(p => productIds.Contains(p.ProductId))
                .ToDictionary(p => p.ProductId, p => p.Price);

            if (products.Count != productIds.Count)
            {
                return Result<string>.Failure(ErrorCode.NotFound, "One or more products not found.");
            }

            existingOrder.CustomerId = orderDto.CustomerID;
            existingOrder.ShipmentId = orderDto.ShipmentId;
            existingOrder.Order_Date = DateTime.UtcNow;
            existingOrder.Order_Items = orderDto.Items.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = products[oi.ProductId]
            }).ToList();

            existingOrder.Total_Price = existingOrder.Order_Items.Sum(oi => oi.Price * oi.Quantity);

            orderRepo.Update(id, existingOrder);
            orderRepo.Save();

            return Result<string>.Success("Order updated successfully.");
        }

        public Result<string> DeleteOrder(int id)
        {
            var order = orderRepo.GetById(id);
            if (order == null)
            {
                return Result<string>.Failure(ErrorCode.NotFound, "Order not found.");
            }

            orderRepo.Remove(id);
            orderRepo.Save();

            return Result<string>.Success("Order deleted successfully.");
        }
    }
}
