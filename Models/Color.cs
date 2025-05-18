namespace HandmadeMarket.Models
{
    public class Color
    {
        public int ColorID { get; set; }
        public string? ColorName { get; set; } 
        public string? ColorCode { get; set; }
        public List<Product> products  { get; set; }

    }
}
