using HandmadeMarket.DTO.ProductDTOs;
namespace HandmadeMarket.DTO.CategoryDTOs
{
    public class CategoryWithProductDTO
    {
        public int categoryId { get; set; }
        public string? name { get; set; }
        public virtual List<ProductDTO>? Products { get; set; }
    }
}
