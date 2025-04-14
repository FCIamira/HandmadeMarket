using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeMarket.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string? SKU { get; set; }
        public string? description { get; set; }
        public string? name { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string? image { get; set; }
        [ForeignKey("category")]
        public int categoryId { get; set; }
        [ForeignKey("seller")]
        public int sellerId { get; set; }
        public virtual Seller? seller { get; set; }
        public virtual Category? category { get; set; }
    }
}
