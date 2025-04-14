namespace HandmadeMarket.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
       public virtual Customer? Customer { get; set; }
        public virtual List<Order>? Orders { get; set; }


    }
}
