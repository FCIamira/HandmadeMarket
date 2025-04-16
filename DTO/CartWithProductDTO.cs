namespace HandmadeMarket.DTO
{
    public class CartWithProductDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ?ProductName { get; set; }

    }
}
