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
        public string sellerId { get; set; }

        public bool HasSale { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalePercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PriceAfterSale { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }

    }
}
