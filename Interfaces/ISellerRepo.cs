using HandmadeMarket.Data;

namespace HandmadeMarket.Interfaces
{
    public interface ISellerRepo: IGenericRepo<Seller>
    {
        IEnumerable<Seller>  GetAllSellersWithProducts();
        Seller GetSellerWithProductsByStoreName(string storeName);
        //Seller DeleteSellerWithProductsById(string id);
        Seller GetSellerWithProductsById(string id);
        Seller GetSellerById(string id);
        Seller GetSellerByProductId(int id);
    }
}
