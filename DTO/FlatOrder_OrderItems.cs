namespace HandmadeMarket.DTO
{
    public class FlatOrder_OrderItems
    {
       
        public DateTime Order_Date { get; set; }
        public decimal Total_Price { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public string ProductName { get; set; }
       

    }
}
