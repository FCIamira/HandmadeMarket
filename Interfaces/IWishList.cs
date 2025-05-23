using HandmadeMarket.Data;

namespace HandmadeMarket.Interfaces
{
    public interface IWishList:IGenericRepo<Wishlist>
    {
        public List<Wishlist> GetWishLists();
        public Wishlist GetWishListById(int id);
        public bool Exists(int productId, string customerId);
    }
}
