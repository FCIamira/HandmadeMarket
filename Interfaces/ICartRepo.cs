using HandmadeMarket.Data;
using HandmadeMarket.DTO.CartDTOs;

namespace HandmadeMarket.Interfaces
{
    public interface ICartRepo:IGenericRepo<Cart>
    {
        public List<CartWithProductDTO> CategoryDTO();

    }
}
