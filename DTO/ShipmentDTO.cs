namespace HandmadeMarket.DTO
{
    public class ShipmentDTO
    {
        public int Id { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public int CustomerId { get; set; }
        public List<OrderDTO>? Orders { get; set; }
    }
}
