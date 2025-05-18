namespace HandmadeMarket.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public int? width { get; set; }
        public int? height { get; set; }
        public int? Length { get; set; }
        [ForeignKey("Category")]
        public int sizeCategoryID { get; set; }
        public virtual Category Category{ get; set; }
        public List<Product> Products { get; set; }



    }
}
