namespace HandmadeMarket.DTO.SellersDTOs
{
    public class SellerWithProductsDTO
    {
        public string sellerId { get; set; }
        public string? storeName { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public DateTime createdAt { get; set; }
        public virtual List<ProductDTO>? Products { get; set; }
    }
}
