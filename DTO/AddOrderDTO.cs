namespace HandmadeMarket.DTO
{
    public class AddOrderDTO
    {
        public int CustomerID { get; set; }
        public int ShipmentId { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
}
