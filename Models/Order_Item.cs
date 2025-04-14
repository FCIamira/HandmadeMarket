namespace HandmadeMarket.Models
{
    public class Order_Item
    {
        [Key]
        public int OrderItemId { get; set; } // Part of composite PK
        public int OrderId { get; set; }    // Part of composite PK
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        //[ForeignKey("ProductId")]
        //public Product Product { get; set; }
        //public int ProductId { get; set; }
       



    }
}
