namespace HandmadeMarket.DTO.CartDTOs
{
    public class CartGetAllDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }

    }
}
