namespace HandmadeMarket.DTO
{
    public class AddProductDTO
    {
        public int ProductId { get; set; }

        public string? Description { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public int categoryId { get; set; }
        public int sellerId { get; set; }
    }
}
