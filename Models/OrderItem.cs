namespace HandmadeMarket.Models
{
    public class OrderItem
    {
       [Key]
      
        public int OrderItemId { get; set; } 
        [ForeignKey("Order")]
        public int OrderId { get; set; }    
      
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal Price { get; set; }
       
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }
        public Order Order { get; set; }

    }
}
