using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeMarket.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? SKU { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Category")]
        public int categoryId { get; set; }
        [ForeignKey("Seller")]
        public int sellerId { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual Category? Category { get; set; }

    }
}
