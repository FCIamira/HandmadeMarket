namespace HandmadeMarket.DTO
{
    public class TopProductsDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int OrderCount { get; set; }
        public int TotalQuantity { get; set; }
    }
}
