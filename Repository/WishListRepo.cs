namespace HandmadeMarket.Repository
{
    public class WishListRepo:GenericRepo<Wishlist>,IWishList
    {
        private readonly HandmadeContext handmadeContext;
        public WishListRepo(HandmadeContext handmadeContext) : base(handmadeContext)
        {
            this.handmadeContext = handmadeContext;
        }
        public List<WishListDTO> GetWishLists()
        {
            List<WishListDTO> wishLists = handmadeContext.Wishlists.Select(w => new WishListDTO {
            Id=w.Id,
            ProductDescription=w.Product.Description,
            ProductName=w.Product.Name,
            ProductPrice=w.Product.Price,
            Image=w.Product.Image,
            ProductId=w.Product.ProductId

            
            }).ToList();

            return wishLists;
        }
        public WishListDTO GetWishListById(int id)
        {
            WishListDTO wishList = handmadeContext.Wishlists
                .Where(w => w.Id == id)
                .Include(c=>c.Product)
                .Select(w => new WishListDTO
                {
                    Id = w.Id,
                    ProductDescription = w.Product.Description,
                    ProductName = w.Product.Name,
                    ProductPrice = w.Product.Price,
                    Image = w.Product.Image,
                    ProductId = w.Product.ProductId
                })
                .FirstOrDefault();

            return wishList;
        }


    }
}
