
namespace HandmadeMarket.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime Order_Date { get; set; }
        public decimal Total_Price { get; set; }

        //[ForeignKey("CustomerId")]
        //public Customer Customer { get; set; }
        //public int CustomerId { get; set; }
        //[ForeignKey("PaymentId")]
        //public Payment Payment { get; set; }
        //public int PaymentId { get; set; }
        //[ForeignKey("ShipmentId")]
        //public Shipment Shipment { get; set; }
        //public int ShipmentId { get; set; }

        public List<Order_Item> Order_Items { get; set; }

    }
}
