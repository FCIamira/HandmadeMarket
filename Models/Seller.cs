namespace HandmadeMarket.Models
{
    public class Seller
    {
        public int sellerId { get; set; }
        //[Key]
        //public string UserId { get; set; }
        public string? storeName { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public DateTime createdAt { get; set; }
        public virtual List<Product>? Products { get; set; }
        //[ForeignKey("UserId")]
        //public virtual ApplicationUser? User { get; set; }
    }
}
