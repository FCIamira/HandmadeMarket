namespace HandmadeMarket.Models
{
    public class Wishlist
    {
        [Key]
      
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
