
using HandmadeMarket.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandmadeMarket.Repository
{
    public class SellerRepo : GenericRepo<Seller>, ISellerRepo
    {
        HandmadeContext context;
        public SellerRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }

        public bool DeleteSellerWithProductsById(string id)
        {
            var seller = context.Sellers
                .Include(s => s.Products)
                .FirstOrDefault(s => s.UserId == id);

            if (seller == null)
                return false;

            context.Sellers.Remove(seller);
            context.SaveChanges();
            return true;
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
            Seller? seller = context.Sellers
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
