using HandmadeMarket.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

            return Result<List<FlatOrder_OrderItems>>.Success(orderDTO);

        }

        public Result<FlatOrder_OrderItems> GetById(int id)
        {
            Order order = orderRepo.GetById(id);
            if (order == null)
            {
                return Result<FlatOrder_OrderItems>.Failure("Not Found");
            }
            FlatOrder_OrderItems FlatOrder = order.Order_Items.Select(oi => new FlatOrder_OrderItems
            {
                Order_Date = order.Order_Date,
                Total_Price = order.Total_Price,
                CustomerName = order.Customer.FirstName,
                PricePerItem = oi.Price,
                Quantity = oi.Quantity,
                ProductName = oi.Product.Name,
            }).FirstOrDefault();

            return Result<FlatOrder_OrderItems>.Success(FlatOrder);
        }

        public Result<string> CreateOrder(AddOrderDTO orderDto)
        {
            if (orderDto == null || orderDto.Items == null || !orderDto.Items.Any())
            {
                return Result<string>.Failure("Bad Request");
            }

            var productIds = orderDto.Items.Select(i => i.ProductId).ToList();
            var products = productRepo.GetAll()
                            .Where(p => productIds.Contains(p.ProductId))
                            .ToDictionary(p => p.ProductId, p => p.Price);

            if (products.Count != productIds.Count)
            {
                return Result<string>.Failure("One or more products not found.");
            }

            var orderItems = orderDto.Items.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = products[oi.ProductId]
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

            return Result<string>.Success("Created");
        }


        public Result<string> UpdateOrder(int id, AddOrderDTO orderDto)
        {
            if (orderDto == null || orderDto.Items == null || !orderDto.Items.Any())
            {
                return Result<string>.Failure("Bad request");
            }

            // Get the existing order from the DB
            var existingOrder = orderRepo.GetById(id);
            if (existingOrder == null)
            {
                return Result<string>.Failure("Order not found.");
            }

            // Get all product IDs from the order
            var productIds = orderDto.Items.Select(i => i.ProductId).ToList();

            // Get actual product prices from the database
            var products = productRepo.GetAll()
                            .Where(p => productIds.Contains(p.ProductId))
                            .ToDictionary(p => p.ProductId, p => p.Price);

            if (products.Count != productIds.Count)
            {
                return Result<string>.Failure("One or more products not found.");
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

            return Result<string>.Success("Updated");
        }

        public Result<string> DeleteOrder(int id)
        {
            var order = orderRepo.GetById(id);
            if (order == null)
            {
                return Result<string>.Failure("Order not found.");
            }
            orderRepo.Remove(id);
            orderRepo.Save();
            return Result<string>.Success("Deleted");
        }
    }
}
