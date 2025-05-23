namespace HandmadeMarket.DTO.OrderItemDTOs
{
    public class OrderItemsWithOrderDetails
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public string CustomerName { get; set; }


        public string CustomerAddress { get; set; }

        public DateTime ShipmentDate { get; set; }

    }
}
