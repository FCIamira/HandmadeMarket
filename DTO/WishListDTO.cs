namespace HandmadeMarket.DTO
{
    public class WishListDTO
    {
        public int Id { get; set; }   
        public int ProductId { get; set; }
        public string ?ProductName { get; set; }
        public string ?ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string? Image { get; set; }

    }
}
