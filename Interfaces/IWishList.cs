namespace HandmadeMarket.Interfaces
{
    public interface IWishList:IGenericRepo<Wishlist>
    {
        public List<WishListDTO> GetWishLists();
        public WishListDTO GetWishListById(int id);
    }
}
