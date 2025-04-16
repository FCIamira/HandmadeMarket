
namespace HandmadeMarket.Models
{
    public class Order
    {
        [Key]
     
        public int OrderId { get; set; }
        public DateTime Order_Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal Total_Price { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Shipment")]
        public int ShipmentId { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Shipment? Shipment { get; set; }

        public List<OrderItem>? Order_Items { get; set; }

    }
}
