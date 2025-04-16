namespace HandmadeMarket.Repository
{
    public class WishListRepo:GenericRepo<Wishlist>,IWishList
    {
        private readonly HandmadeContext handmadeContext;
        public WishListRepo(HandmadeContext handmadeContext) : base(handmadeContext)
        {
            this.handmadeContext = handmadeContext;
        }
    }
}
