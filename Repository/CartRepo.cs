namespace HandmadeMarket.Repository
{
    public class CartRepo:GenericRepo<Cart>,ICartRepo
    {
        private readonly HandmadeContext handmadeContext;

        public CartRepo(HandmadeContext handmadeContext) : base(handmadeContext)
        {
            this.handmadeContext = handmadeContext;
        }

        public List<CartWithProductDTO> CategoryDTO()
        {

            List<CartWithProductDTO> carts = handmadeContext.Carts.Select(c => new CartWithProductDTO
            {
                Id = c.Id,
                ProductId = c.ProductId,
                ProductName = c.Product.Name,
                Quantity = c.Quantity,

            }
            ).ToList();
            return carts;
        }
    }
}
