namespace HandmadeMarket.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; } // Part of composite PK
        [ForeignKey("Order")]
        public int OrderId { get; set; }    // Part of composite PK
      
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
       
        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
       



    }
}
