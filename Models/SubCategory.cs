namespace HandmadeMarket.Models
{
    public class SubCategory
    {
        public int categoryId { get; set; }
        public string? name { get; set; }
        public int sizeFlag { get; set; } = 0;
        [ForeignKey("Category")]
        public int CatID { get; set; }
        public virtual Category Category { get; set; }
        public  List<Product>? Products { get; set; }

    }
}
