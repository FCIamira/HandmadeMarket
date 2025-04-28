﻿
using HandmadeMarket.Context;

namespace HandmadeMarket.Repository
{
    public class OrderItemRepo: GenericRepo<OrderItem>, IOrderItemRepo
    {
        HandmadeContext context;
        public OrderItemRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
           IEnumerable<OrderItem> items = context.Items
                .Include(o=>o.Order)
                .Include(p=>p.Product)
                .Where(o=>o.OrderId == orderId)
                .ToList();
            return items;
        }

        public OrderItem GetById(int id)
        {
            OrderItem orderItem = context.Items
                .Include(o => o.Order)
                .Include(p => p.Product)
                .FirstOrDefault(o => o.OrderItemId == id);
            return orderItem;
        }

        public IEnumerable<OrderItem> GetAllBySellerId(string sellerId)
        {
            //return handmadeContext.Orders.Where(o => o.Order_Items[0].Product.sellerId==sellerId).ToList(); 
            return context.Items.Where(i=>i.Product.sellerId==sellerId).ToList();
        }
    }
    
}
