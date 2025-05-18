namespace HandmadeMarket.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string? name { get; set; }
<<<<<<< HEAD
        public virtual List<Product>? Products {  get; set; } 
=======
        public virtual List<Size>? Sizes {  get; set; } 
        public virtual List<SubCategory> SubCategories { get; set; }

>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
    }
}
