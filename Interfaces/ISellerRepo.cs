namespace HandmadeMarket.Interfaces
{
    public interface ISellerRepo: IGenericRepo<Seller>
    {
        IEnumerable<Seller>  GetAllSellersWithProducts();
        Seller GetSellerWithProductsByStoreName(string storeName);
        SellerWithProductsDTO DeleteSellerWithProductsById(int id);
        Seller GetSellerByProductId(int id);
    }
}
