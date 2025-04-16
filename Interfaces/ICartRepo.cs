namespace HandmadeMarket.Interfaces
{
    public interface ICartRepo:IGenericRepo<Cart>
    {
        public List<CartWithProductDTO> CategoryDTO();
    }
}
