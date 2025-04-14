namespace HandmadeMarket.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public Customer? Customer { get; set; }
    }
}
