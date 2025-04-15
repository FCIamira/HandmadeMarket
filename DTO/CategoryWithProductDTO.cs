namespace HandmadeMarket.DTO
{
    public class CategoryWithProductDTO
    {
        public int categoryId { get; set; }
        public string? name { get; set; }
        public virtual List<ProductDTO>? Products { get; set; }
    }
}
