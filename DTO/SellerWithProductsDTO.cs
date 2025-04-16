namespace HandmadeMarket.DTO
{
    public class SellerWithProductsDTO
    {
        public int sellerId { get; set; }
        public string? storeName { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public DateTime createdAt { get; set; }
        public virtual List<ProductDTO>? Products { get; set; }
    }
}
