namespace HandmadeMarket.Models
{
    public class Cart
    {
        [Key]
  
        public int Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product ?Product { get; set; }
        public Customer ?Customer { get; set; }
    }
}
