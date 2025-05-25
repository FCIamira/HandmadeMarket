using HandmadeMarket.Data;
using HandmadeMarket.DTO.CartDTOs;

namespace HandmadeMarket.Interfaces
{
    public interface ICartRepo:IGenericRepo<Cart>
    {
        public List<CartWithProductDTO> CategoryDTO();
        public IEnumerable<Cart> GetByUserId(string userId);
        Task<bool> IsProductInCartAsync(int productId);



    }
}
