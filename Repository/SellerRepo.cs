
namespace HandmadeMarket.Repository
{
    public class SellerRepo : GenericRepo<Seller>, ISellerRepo
    {
        HandmadeContext context;
        public SellerRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }

        public SellerWithProductsDTO DeleteSellerWithProductsById(string id)
        {
            var sellerData = context.Sellers
                .Where(s => s.UserId == id)
                .Select(s => new SellerWithProductsDTO
                {
                    sellerId = s.UserId,
                    storeName = s.storeName,
                    email = s.email,
                    phoneNumber = s.phoneNumber,
                    createdAt = s.createdAt,
                    Products = s.Products.Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    }).ToList()
                })
                .FirstOrDefault();
            context.Products
                .Where(p => p.sellerId == id)
                .ExecuteDelete();

            context.Sellers
                .Where(s => s.UserId == id)
                .ExecuteDelete();

            return sellerData;
        }
        public IEnumerable<Seller> GetAllSellersWithProducts()
        {
            var sellers = context.Sellers
                .Include(s => s.Products)
                .ToList();
               
            return sellers;
        }

        public Seller GetSellerById(string id)
        {
            Seller? seller = context.Sellers
                .Where(s => s.UserId == id)
                .Include(s => s.Products)
                .FirstOrDefault();
            return seller;
        }

        public Seller GetSellerByProductId(int id)
        {
            Seller? seller= context.Sellers
                .Where(s => s.Products.Any(p => p.ProductId == id))
                .FirstOrDefault();
            return seller;
        }

        public Seller GetSellerWithProductsById(string id)
        {
            Seller? sellers =  context.Sellers
                .Where(s => s.UserId == id)
                .Include(s => s.Products)
                .FirstOrDefault();
            return sellers;
        }

        public Seller GetSellerWithProductsByStoreName(string storeName)
        {
            Seller? sellers = context.Sellers
                .Where(s => s.storeName.ToLower().Contains(storeName.ToLower()))
                .FirstOrDefault();
            return sellers;
        }
    }
}
