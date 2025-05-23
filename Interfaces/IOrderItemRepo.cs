﻿using HandmadeMarket.Data;

namespace HandmadeMarket.Interfaces
{
    public interface IOrderItemRepo : IGenericRepo<OrderItem>
    {
        IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId);
        IEnumerable<OrderItem> GetAllBySellerId(string sellerId, int pageNumber , int pageSize );
    }
}
