namespace HandmadeMarket.DTO
{
    public class ViewOrderItemDto
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }
}
