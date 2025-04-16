
namespace HandmadeMarket.Repository
{
    public class SellerRepo : GenericRepo<Seller>, ISellerRepo
    {
        HandmadeContext context;
        public SellerRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }

        public void AddSeller(SellerDTO sellerDTO)
        {
            Seller seller = new Seller
            {
                sellerId = sellerDTO.sellerId,
                storeName = sellerDTO.storeName,
                email = sellerDTO.email,
                phoneNumber = sellerDTO.phoneNumber,
                createdAt = DateTime.Now
            };
            context.Sellers.Add(seller);
        }

        public SellerWithProductsDTO DeleteSellerWithProductsById(int id)
        {
            var sellerData = context.Sellers
                .Where(s => s.sellerId == id)
                .Select(s => new SellerWithProductsDTO
                {
                    sellerId = s.sellerId,
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
                .Where(s => s.sellerId == id)
                .ExecuteDelete();

            return sellerData;
        }

        public void EditSeller(int id, SellerDTO sellerDTO)
        {
            Seller? seller = context.Sellers
                .Where(s => s.sellerId == id)
                .FirstOrDefault();

            seller.storeName = sellerDTO.storeName;
            seller.email = sellerDTO.email;
            seller.phoneNumber = sellerDTO.phoneNumber;
            

        }

        public IQueryable<SellerWithProductsDTO> GetAllSellersWithProducts()
        {
            var sellers = context.Sellers
                .Select(s => new SellerWithProductsDTO
                {
                    sellerId = s.sellerId,
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
                });
            return sellers;
        }

        public SellerWithProductsDTO GetSellerWithProductsById(int id)
        {
            SellerWithProductsDTO? sellers =  context.Sellers
                .Where(s => s.sellerId == id)
                .Select(s => new SellerWithProductsDTO
                {
                    sellerId = s.sellerId,
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
                }).FirstOrDefault();
            return sellers;
        }

        public SellerWithProductsDTO GetSellerWithProductsByStoreName(string storeName)
        {
            SellerWithProductsDTO? sellers = context.Sellers
                .Where(s => s.storeName.ToLower().Contains(storeName.ToLower()))
                .Select(s => new SellerWithProductsDTO
                {
                    sellerId = s.sellerId,
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
                }).FirstOrDefault();
            return sellers;
        }
    }
}
