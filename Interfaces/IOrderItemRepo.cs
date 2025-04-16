namespace HandmadeMarket.Interfaces
{
    public interface IOrderItemRepo : IGenericRepo<OrderItem>
    {
        IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId);
       
    }
}
