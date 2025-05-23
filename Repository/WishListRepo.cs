namespace HandmadeMarket.Repository
{
    public class WishListRepo:GenericRepo<Wishlist>,IWishList
    {
        private readonly HandmadeContext handmadeContext;
        public WishListRepo(HandmadeContext handmadeContext) : base(handmadeContext)
        {
            this.handmadeContext = handmadeContext;
        }
        #region GetWishLists
        public List<Wishlist> GetWishLists()
        {
            return handmadeContext.Wishlists.Include(w => w.Product).ToList();
        } 
        #endregion

        #region GetWishListById
        public Wishlist? GetWishListById(int id)
        {
            return handmadeContext.Wishlists
                .Include(w => w.Product)
                .FirstOrDefault(w => w.Id == id);
        }

        #endregion

        #region Exists
        public bool Exists(int productId, string customerId)
        {
            return handmadeContext.Wishlists.Any(w => w.ProductId == productId && w.CustomerId == customerId);
        }


        #endregion
    }
}
