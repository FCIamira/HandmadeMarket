namespace HandmadeMarket.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime Order_Date { get; set; }
        public decimal Total_Price { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemDTO>? Order_Items { get; set; }
    }
}
