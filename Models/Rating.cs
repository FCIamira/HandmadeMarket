namespace HandmadeMarket.Models
{
    public class Rating
    {
        public int RatingId { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }

        public string? Comment { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
