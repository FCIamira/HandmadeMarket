using HandmadeMarket.Data;
using HandmadeMarket.DTO.CartDTOs;

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
                //ProductName = c.Product.Name,
                Quantity = c.Quantity,

            }
            ).ToList();
            return carts;
        }
        public override IEnumerable<Cart> GetAll()
        {
            return handmadeContext.Carts.Include(c => c.Product).ToList();
        }

        public IEnumerable<Cart> GetByUserId(string userId)
        {
            return handmadeContext.Carts
                .Include(c => c.Product)
                .Where(c => c.CustomerId == userId)
                .ToList();
        }


        public override Cart GetById(int id)
        {
            return handmadeContext.Carts
                .Include(c => c.Product)
                .FirstOrDefault(c => c.Id == id);
        }


    }
}
