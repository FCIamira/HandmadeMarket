namespace HandmadeMarket.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string? name { get; set; }
        public virtual List<Product>? Products {  get; set; } 
    }
}
